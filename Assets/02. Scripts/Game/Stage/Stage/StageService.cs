using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInfo
{
    public int Index;
    public Stage Stage;
}

public class StageService : MonoBehaviour
{
    #region Variables
    [Header("스테이지 목록")]
    [SerializeField] private List<StageInfo> m_stage_list;
    private Dictionary<int, Stage> m_stage_dict;
    #endregion Variables

    private void Awake()
    {
        m_stage_dict = new();
        Initialize();
    }

    private void Initialize()
    {
        foreach (var stage_info in m_stage_list)
        {
            if (!m_stage_dict.TryAdd(stage_info.Index, stage_info.Stage))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{stage_info.Index}를 딕셔너리에 추가하는 과정에서 동일한 키를 발견했습니다.");
#endif
                return;
            }
        }
    }

    public Stage GetStage(int index)
    {
        return m_stage_dict.TryGetValue(index, out var stage) ? stage : null;
    }
}
