using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string m_data_file_path;

    private UserData m_user_data;
    public UserData Data
    {
        get => m_user_data;
        set => m_user_data = value;
    }

    private new void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Initialize()
    {
        m_data_file_path = Path.Combine(Application.persistentDataPath, "UserData.json");

        if(File.Exists(m_data_file_path) is false)
        {
            Data = new UserData();
            SaveJson();
        }
        else
        {
            LoadJson();
        }
    }

    private void LoadJson()
    {
        var json_data = File.ReadAllText(m_data_file_path);
        Data = JsonUtility.FromJson<UserData>(json_data);
    }

    public void SaveJson()
    {
        var json_data = JsonUtility.ToJson(Data);
        File.ReadAllText(json_data);
    }
}
