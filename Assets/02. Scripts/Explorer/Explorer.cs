using UnityEngine;

[CreateAssetMenu(fileName = "New Explorer", menuName = "Scriptable Object/Create Explorer")]
public class Explorer : ScriptableObject
{
    [Header("탐험가의 고유한 ID")]
    [SerializeField] private int m_private_id;
    public int ID { get => m_private_id; }

    [Header("탐험가의 고유한 이름")]
    [SerializeField] private string m_explorer_name;
    public string Name { get => m_explorer_name; }

    [Header("탐험가의 등급")]
    [SerializeField] private ExplorerGrade m_explorer_grade;
    public ExplorerGrade Grade { get => m_explorer_grade; }

    [Header("탐험가의 기본 훈련 비용")]
    [SerializeField] private int m_upgrade_cost;
    public int UpgradeCost { get => m_upgrade_cost; } 

    [Header("탐험가의 최대 훈련 가능 횟수")]
    [SerializeField] private int m_max_upgrade_count;
    public int MaxUpgrade { get => m_max_upgrade_count; }

    [Header("탐험가의 특성")]
    [SerializeField] private ExplorerType m_explorer_type;
    public ExplorerType Type { get => m_explorer_type; }

    [Header("탐험가의 구매가")]
    [SerializeField] private int m_explorer_price;
    public int Price { get => m_explorer_price; }

    [Header("탐험가의 체력")]
    [SerializeField] private float m_explorer_hp;
    public float HP { get => m_explorer_hp; }

    [Header("탐험가의 성장 체력")]
    [SerializeField] private float m_growth_hp;
    public float GrowthHP { get => m_growth_hp; }

    [Header("탐험가의 공격력")]
    [SerializeField] private float m_explorer_atk;
    public float ATK { get => m_explorer_atk; }

    [Header("탐험가의 성장 공격력")]
    [SerializeField] private float m_growth_atk;
    public float GrowthATK { get => m_growth_atk; }

    [Header("탐험가의 사거리")]
    [SerializeField] private float m_atk_range;
    public float Range { get => m_atk_range; }

    [Header("탐험가의 소환 비용")]
    [SerializeField] private int m_cost;
    public int Cost { get => m_cost; }

    [Header("탐험가의 이미지")]
    [SerializeField] private Sprite m_explorer_sprite;
    public Sprite Image { get => m_explorer_sprite; }
}