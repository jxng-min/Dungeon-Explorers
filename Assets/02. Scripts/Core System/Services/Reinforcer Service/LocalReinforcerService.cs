using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ReinforcerService
{
    #region Serialization
    [System.Serializable]
    public class ReinforcementData
    {
        public ReinforcementType Type;
        public int Level;

        public ReinforcementData(ReinforcementType type, int level)
        {
            Type = type;
            Level = level;
        }
    }

    [System.Serializable]
    public class DataWrapper
    {
        public ReinforcementData[] Data;

        public DataWrapper()
        {
            var temp_data_list = new List<ReinforcementData>();

            foreach (ReinforcementType type in Enum.GetValues(typeof(ReinforcementType)))
            {
                temp_data_list.Add(new(type, 1));
            }

            Data = temp_data_list.ToArray();
        }

        public DataWrapper(ReinforcementData[] data)
        {
            Data = data;
        }
    }
    #endregion Serialization

    public class LocalReinforcerService : IReinforcerService
    {
        private string m_local_data_path;

        private Dictionary<ReinforcementType, int> m_reinforcement_dict;

        public event Action<ReinforcementType, int> OnUpdatedReinforcement;

        public LocalReinforcerService()
        {
            m_reinforcement_dict = new();

            Load();
        }
        
        public int GetField(ReinforcementType type)
        {
            return m_reinforcement_dict.TryGetValue(type, out var field) ? field : -1;
        }

        public void UpgradeField(ReinforcementType type, int amount = 1)
        {
            if (m_reinforcement_dict.ContainsKey(type))
            {
                m_reinforcement_dict[type] += amount;

                OnUpdatedReinforcement?.Invoke(type, m_reinforcement_dict[type]);
            }
        }

        public bool Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "ReinforcerData.json");

            DataWrapper reinforcement_data;
            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);

                reinforcement_data = JsonUtility.FromJson<DataWrapper>(json_data);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 없으므로 강화 데이터를 새롭게 생성합니다.</color>");
#endif
                reinforcement_data = new DataWrapper();
            }

            foreach (var data in reinforcement_data.Data)
            {
                if (!m_reinforcement_dict.TryAdd(data.Type, data.Level))
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{data.Type}을 딕셔너리에 추가하는 과정에서 동일한 데이터가 존재했습니다.");
#endif
                    return false;
                }
            }

            return true;
        }

        public void Save()
        {
            var data = m_reinforcement_dict
                        .Select(kvp => new ReinforcementData(kvp.Key, kvp.Value))
                        .ToArray();

            var data_wrapper = new DataWrapper(data);
            var json_data = JsonUtility.ToJson(data_wrapper, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
    }
}