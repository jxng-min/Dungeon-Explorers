using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExplorerDataManager : Singleton<ExplorerDataManager>
{
    [Header("탐험가의 스크립터블 오브젝트 목록")]
    [SerializeField] private Explorer[] m_explorer_list;

    private string m_file_path;

    private Dictionary<int, Explorer> m_explorer_dict;
    private Dictionary<int, string> m_description_dict;

    private new void Awake()
    {
        base.Awake();

        m_explorer_dict = new();
        m_description_dict = new();

        LoadJson();   
    }

    private void LoadJson()
    {
        m_file_path = Path.Combine(Application.streamingAssetsPath, "ExplorerData.json");

        if(File.Exists(m_file_path) is false)
        {
            Debug.Log(m_file_path + "가 존재하지 않습니다.");
        }

        var json_data = File.ReadAllText(m_file_path);
        var explorer_list = JsonUtility.FromJson<ExplorerList>(json_data);

        foreach(var explorer in explorer_list.Datas)
        {
            foreach(var target in m_explorer_list)
            {
                if(explorer.ID == target.ID)
                {
                    if(m_explorer_dict.TryAdd(explorer.ID, target) is false)
                    {
                        Debug.Log("m_explorer_dict에 이미 중복된 데이터가 존재합니다.");
                    }

                    break;
                }
            }

            if(m_description_dict.TryAdd(explorer.ID, explorer.Description) is false)
            {
                Debug.Log("m_description_dict에 이미 중복된 데이터가 존재합니다.");
            }
        }
    }

    public Explorer GetExplorer(int id)
    {
        return m_explorer_dict.ContainsKey(id) ? m_explorer_dict[id] : null;
    }

    public string GetDescription(int id)
    {
        return m_description_dict.ContainsKey(id) ? m_description_dict[id] : null;
    }

    public int GetSize()
    {
        return m_explorer_list.Length;
    }
}