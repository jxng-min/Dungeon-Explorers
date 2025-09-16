using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InventoryService
{
    #region Serialization
    [System.Serializable]
    public class UnitData
    {
        public UnitCode Code;
        public int Upgrade;

        public UnitData(UnitCode code, int upgrade_count)
        {
            Code = code;
            Upgrade = upgrade_count;
        }        
    }

    [System.Serializable]
    public class DataWrapper
    {
        public int Money;
        public UnitData[] UnitData;

        public DataWrapper(int money, UnitData[] data)
        {
            Money = money;
            UnitData = data;
        }
    }
    #endregion Serialization

    public class LocalInventoryService : IInventoryService
    {
        private string m_local_data_path;

        private int m_money;
        private List<UnitData> m_unit_list;

        public event Action<int> OnUpdatedMoney;
        public event Action<UnitData> OnUpdatedUnit;

        public int Money => m_money;
        public List<UnitData> Units => m_unit_list;

        public LocalInventoryService()
        {
            m_unit_list = new();

            Load();
        }

        public void Initialize()
        {
            OnUpdatedMoney?.Invoke(Money);
        }

        public bool HasUnit(UnitCode code)
        {
            foreach (var unit in m_unit_list)
            {
                if (unit.Code == code)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddUnit(UnitCode code, int upgrade_count = 1)
        {
            if (HasUnit(code))
            {
                return false;
            }

            var unit_data = new UnitData(code, upgrade_count);
            m_unit_list.Add(unit_data);

            OnUpdatedUnit?.Invoke(unit_data);

            return true;
        }

        public UnitData GetUnit(UnitCode code)
        {
            foreach (var unit in m_unit_list)
            {
                if (unit.Code == code)
                {
                    return unit;
                }
            }

            return null;
        }

        public void UpdateMoney(int amount)
        {
            m_money += amount;
            m_money = Mathf.Clamp(m_money, 0, int.MaxValue);

            OnUpdatedMoney?.Invoke(m_money);
        }

        public bool Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "InvenData.json");

            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);

                var inven_data = JsonUtility.FromJson<DataWrapper>(json_data);
                if (inven_data == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{m_local_data_path}의 형식에 오류가 있습니다.");
#endif
                    return false;
                }

                m_money = inven_data.Money;
                foreach (var unit in inven_data.UnitData)
                {
                    m_unit_list.Add(unit);
                }

                return true;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 없으므로 인벤토리 데이터를 새롭게 생성합니다.</color>");
#endif
                m_money = 100000;
                m_unit_list.Add(new(0, 1));

                return false;
            }
        }

        public void Save()
        {
            var data = m_unit_list.ToArray();
            var wrapper = new DataWrapper(m_money, data);

            var json_data = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
    }
}