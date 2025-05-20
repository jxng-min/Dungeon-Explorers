using System.IO;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    private string m_setting_data_path;

    private SettingData m_setting_data;
    public SettingData Data
    {
        get => m_setting_data;
        private set => m_setting_data = value;
    }

    private new void Awake()
    {
        base.Awake();

        m_setting_data_path = Path.Combine(Application.persistentDataPath, "SettingData.json");

        if (File.Exists(m_setting_data_path))
        {
            LoadJson();
        }
        else
        {
            Data = new SettingData();
        }
    }

    private void LoadJson()
    {
        var json_data = File.ReadAllText(m_setting_data_path);
        Data = JsonUtility.FromJson<SettingData>(json_data);
    }

    public void SaveJson()
    {
        var json_data = JsonUtility.ToJson(Data);
        File.WriteAllText(m_setting_data_path, json_data);
    }
}
