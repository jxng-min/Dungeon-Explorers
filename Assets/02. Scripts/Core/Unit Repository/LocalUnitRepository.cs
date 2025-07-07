using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnitRepository
{
    #region Serialization
    [System.Serializable]
    public class Data
    {
        public UnitCode Code;
        public string Name;
        public string Description;
    }

    [System.Serializable]
    public class DataWrapper
    {
        public Data[] Data;
    }
    #endregion Serialization

    public class LocalUnitRepository : IUnitRepository
    {
        #region Variables
        private Dictionary<UnitCode, string> m_name_dictionary;
        private Dictionary<UnitCode, string> m_description_dictionary;
        #endregion Variables

        public LocalUnitRepository()
        {
            m_name_dictionary = new();
            m_description_dictionary = new();

            Initialize();
        }

        #region Helper Methods
        private void Initialize()
        {
            var local_data_path = Path.Combine(Application.streamingAssetsPath, "UnitData.json");

            if (File.Exists(local_data_path))
            {
                var json_data = File.ReadAllText(local_data_path);
                var unit_data = JsonUtility.FromJson<DataWrapper>(json_data);
                if (unit_data == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{local_data_path}의 형식에 오류가 있습니다.");
#endif
                }

                foreach (var unit in unit_data.Data)
                {
                    m_name_dictionary.Add(unit.Code, unit.Name);
                    m_description_dictionary.Add(unit.Code, unit.Description);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{local_data_path}가 존재하지 않습니다.");
#endif
            }
        }

        public string GetName(UnitCode code)
        {
            return m_name_dictionary.TryGetValue(code, out var name) ? name : null;
        }

        public string GetDescription(UnitCode code)
        {
            return m_description_dictionary.TryGetValue(code, out var description) ? description : null;
        }
        #endregion Helper Methods;
    }
}