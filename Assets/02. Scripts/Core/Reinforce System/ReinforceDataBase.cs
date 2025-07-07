using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ReinforceDataBase", menuName = "SO/DB/Create ReinforceDataBase")]
public class ReinforceDataBase : ScriptableObject
{
    #region Variables
    [Header("강화 목록에 포함될 요소")]
    [SerializeField] private List<ReinforcementItem> m_reinforcement_list;

    private Dictionary<ReinforcementType, ReinforcementItem> m_reinforcement_dict;
    #endregion Variables

    #region Properties
    public int Count { get => m_reinforcement_list.Count; }
    #endregion Properties

    private void OnEnable()
    {
        m_reinforcement_dict = new();

        foreach (var element in m_reinforcement_list)
        {
            if (!m_reinforcement_dict.TryAdd(element.Type, element))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"ReinforcementDataBase에 {element.Type}을 추가하는 과정에서 동일한 타입이 존재했습니다.");
#endif
            }
        }
    }

    public ReinforcementItem GetItem(ReinforcementType type)
    {
        return m_reinforcement_dict.TryGetValue(type, out var item) ? item : null;
    }
}