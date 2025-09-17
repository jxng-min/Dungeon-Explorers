using UnityEngine;

[CreateAssetMenu(fileName = "New Reinforcement Item", menuName = "SO/Create Reinforcement Item")]
public class ReinforcementItem : ScriptableObject
{
    [Header("강화 타입")]
    [SerializeField] private ReinforcementType m_type;
    public ReinforcementType Type { get => m_type; }

    [Header("강화 이름")]
    [SerializeField] private string m_name;
    public string Name { get => m_name; }

    [Header("강화 이미지")]
    [SerializeField] private Sprite m_image;
    public Sprite Image { get => m_image; }

    [Header("기본 가격")]
    [SerializeField] private int m_default_cost;
    public int DefaultCost { get => m_default_cost; }

    [Header("성장 가격")]
    [SerializeField] private int m_growth_cost;
    public int GrowthCost { get => m_growth_cost; } 

    [Header("최대 강화")]
    [SerializeField] private int m_max_limit;
    public int Limit => m_max_limit;
}
