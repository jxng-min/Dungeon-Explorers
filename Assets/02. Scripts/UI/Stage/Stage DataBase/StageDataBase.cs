using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage DataBase", menuName = "SO/DB/Create Stage DataBase")]
public class StageDataBase : ScriptableObject, IStageDataBase
{
    [Header("스테이지 목록")]
    [SerializeField] private Stage[] m_stage_list;
    private Dictionary<int, Stage> m_stage_dict;


    public int Count => m_stage_list.Length;
    public int Current { get; set; }

#if UNITY_EDITOR
    private void OnEnable()
    {
        Initialize();
    }
#endif

    private void Initialize()
    {
        if(m_stage_list == null)
        {
            return;
        }

        m_stage_dict = new();
        foreach(var stage in m_stage_list)
        {
            m_stage_dict.TryAdd(stage.ID, stage);
        }
    }

    public Stage GetStage(int stage_id)
    {
        if(m_stage_dict == null)
        {
            Initialize();
        }

        return m_stage_dict.TryGetValue(stage_id, out var stage) ? stage : null;
    }
}
