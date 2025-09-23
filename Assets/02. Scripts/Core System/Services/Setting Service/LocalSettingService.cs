using System.IO;
using UnityEngine;

namespace SettingService
{
    #region Serialization
    [System.Serializable]
    public class SettingData
    {
        public bool BGM;
        public float BGMRate;
        public bool SFX;
        public float SFXRate;

        public SettingData()
        {
            BGM = true;
            BGMRate = 0.5f;
            SFX = true;
            SFXRate = 0.5f;
        }
    }
    #endregion Serialization

    public class LocalSettingService : ISettingService
    {
        private string m_local_data_path;

        private SettingData m_setting_data;

        public bool BGM
        {
            get => m_setting_data.BGM;
            set => m_setting_data.BGM = value;
        }

        public float BGMRate
        {
            get => m_setting_data.BGMRate;
            set => m_setting_data.BGMRate = value;
        }

        public bool SFX
        {
            get => m_setting_data.SFX;
            set => m_setting_data.SFX = value;
        }

        public float SFXRate
        {
            get => m_setting_data.SFXRate;
            set => m_setting_data.SFXRate = value;
        }

        public LocalSettingService()
        {
            Load();
        }

        public bool Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "SettingData.json");

            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);
                m_setting_data = JsonUtility.FromJson<SettingData>(json_data);
                if (m_setting_data == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{m_local_data_path}의 형식에 오류가 있습니다.");
#endif
                    return false;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 존재하지 않아 새로운 설정 데이터를 생성합니다.</color>");
#endif
                m_setting_data = new SettingData();
            }

            return true;
        }

        public void Save()
        {
            var json_data = JsonUtility.ToJson(m_setting_data, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
    }
}