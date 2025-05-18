using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExpManager : Singleton<ExpManager>
{
    private string m_exp_data_path;
    private Dictionary<int, int> m_exp_dict;

    private new void Awake()
    {
        base.Awake();

        m_exp_data_path = Path.Combine(Application.streamingAssetsPath, "ExpData.json");
        m_exp_dict = new();
    }

    private void Start()
    {
        LoadJson();   
    }

    private void LoadJson()
    {
        if(File.Exists(m_exp_data_path) is false)
        {
            return;
        }   

        var json_data = File.ReadAllText(m_exp_data_path);
        var exp_data_list = JsonUtility.FromJson<ExpDataList>(json_data);
        foreach(var exp_data in exp_data_list.Exps)
        {
            m_exp_dict.Add(exp_data.m_to_level, exp_data.m_to_exp);
        }
    }

    public int GetExp(int current_level)
    {
        int next_level = current_level + 1;
        return m_exp_dict.ContainsKey(next_level) ? m_exp_dict[next_level] : m_exp_dict[10];
    }
}
