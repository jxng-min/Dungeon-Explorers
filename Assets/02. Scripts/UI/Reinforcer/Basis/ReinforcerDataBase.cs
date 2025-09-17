using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reinforcement DataBase", menuName = "SO/DB/Create Reinforcement DataBase")]
public class ReinforcerDataBase : ScriptableObject, IReinforcerDataBase
{
    [Header("강화 목록")]
    [SerializeField] private List<ReinforcementItem> m_reinforcement_list;

    private Dictionary<ReinforcementType, ReinforcementItem> m_reinforcement_dict;

    public List<ReinforcementItem> List => m_reinforcement_list;

#if UNITY_EDITOR
    private void OnEnable()
    {
        Initialize();
    }
#endif

    private void Initialize()
    {
        if(m_reinforcement_list == null)
        {
            return;
        }

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
        if(m_reinforcement_dict == null)
        {
            Initialize();
        }

        return m_reinforcement_dict.TryGetValue(type, out var item) ? item : null;
    }
}