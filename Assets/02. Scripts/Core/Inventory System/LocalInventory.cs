using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InventoryService
{
    #region Serialization
    [System.Serializable]
    public class Unit
    {
        public UnitCode Code;
        public int Upgrade;

        public Unit(UnitCode code, int upgrade_count)
        {
            Code = code;
            Upgrade = upgrade_count;
        }        
    }

    [System.Serializable]
    public class DataWrapper
    {
        public int Money;
        public Unit[] UnitData;

        public DataWrapper(int money, Unit[] data)
        {
            Money = money;
            UnitData = data;
        }
    }
    #endregion Serialization

    public class LocalInventory : IInventoryService
    {
        #region Variables
        private string m_local_data_path;

        private int m_money;
        private List<Unit> m_unit_list;
        #endregion Variables

        #region Properties
        public int Money
        {
            get => m_money;
            set => m_money = value;
        }

        public List<Unit> Units
        {
            get => m_unit_list;
            set => m_unit_list = value;
        }
        #endregion Properties

        public LocalInventory()
        {
            m_unit_list = new();

            Load();
        }

        #region Helper Methods
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

        public bool TryAdd(UnitCode code, int upgrade_count = 1)
        {
            if (HasUnit(code))
            {
                return false;
            }

            m_unit_list.Add(new Unit(code, upgrade_count));
            return true;
        }

        public Unit GetUnit(UnitCode code)
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

        public void Load()
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
                    return;
                }

                m_money = inven_data.Money;
                foreach (var unit in inven_data.UnitData)
                {
                    m_unit_list.Add(unit);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 없으므로 인벤토리 데이터를 새롭게 생성합니다.</color>");
#endif
                m_money = 100000;
                m_unit_list.Add(new(0, 1));
            }
        }

        public void Save()
        {
            var data = m_unit_list.ToArray();
            var wrapper = new DataWrapper(m_money, data);

            var json_data = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
        #endregion Helper Methods
    }
}