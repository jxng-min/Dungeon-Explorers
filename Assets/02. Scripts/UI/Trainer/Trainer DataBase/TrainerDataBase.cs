using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trainer DataBase", menuName = "SO/DB/Create Trainer DataBase")]
public class TrainerDataBase : ScriptableObject, ITrainerDataBase
{
    [Header("강화 데이터 목록")]
    [SerializeField] private TrainerData[] m_data_list;

    private Dictionary<UnitCode, TrainerData> m_data_dict;

#if UNITY_EDITOR
    private void OnDestroy()
    {
        Initialize();
    }
#endif

    private void Initialize()
    {
        if(m_data_list == null)
        {
            return;
        }

        m_data_dict = new();
        foreach(var data in m_data_list)
        {
            m_data_dict.Add(data.Hero.Code, data);
        }
    }

    public TrainerData GetTrainerData(UnitCode code)
    {
        if(m_data_dict == null)
        {
            Initialize();
        }

        return m_data_dict.TryGetValue(code, out var data) ? data : null;
    }
}
