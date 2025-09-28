using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EXPService
{
    #region Serialization
    [System.Serializable]
    public class ExpData
    {
        public int NextLevel;
        public int NextEXP;
    }

    [System.Serializable]
    public class DataWrapper
    {
        public ExpData[] Data; 
    }
    #endregion Serialization

    public class LocalEXPService : IEXPService
    {
        private Dictionary<int, int> m_exp_dict;
        private const int MAX_LEVEL = 10;

        public LocalEXPService()
        {
            m_exp_dict = new();

            Initialize();
        }

        private void Initialize()
        {
            var local_data_path = Path.Combine(Application.streamingAssetsPath, "EXPData.json");

            if (File.Exists(local_data_path))
            {
                var json_data = File.ReadAllText(local_data_path);
                var exp_data = JsonUtility.FromJson<DataWrapper>(json_data);
                if (exp_data == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{local_data_path}의 형식에 오류가 있습니다.");
#endif
                    return;
                }

                foreach (var data in exp_data.Data)
                {
                    m_exp_dict.Add(data.NextLevel, data.NextEXP);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{local_data_path}가 존재하지 않습니다.");
#endif
            }
        }

        public int GetEXP(int current_level)
        {
            int next_level = current_level + 1;
            return m_exp_dict.TryGetValue(next_level, out var exp) ? exp : m_exp_dict[MAX_LEVEL];
        }
    }
}