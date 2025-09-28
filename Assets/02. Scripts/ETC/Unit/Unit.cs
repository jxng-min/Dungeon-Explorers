using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "SO/Create Unit")]
public class Unit : ScriptableObject
{
    [Header("유닛 기본 정보")]
    [Header("유닛 코드")]
    [SerializeField] private UnitCode m_unit_code;
    public UnitCode Code { get => m_unit_code; }

    [Header("유닛 타입")]
    [SerializeField] private UnitType m_unit_type;
    public UnitType Type { get => m_unit_type; }

    [Header("유닛의 진영")]
    [SerializeField] private Team m_unit_team;
    public Team Team { get => m_unit_team; }

    [Header("유닛 이미지")]
    [SerializeField] private Sprite m_unit_sprite;
    public Sprite Image { get => m_unit_sprite; }

    [Header("유닛이 적으로 인식할 레이어")]
    [SerializeField] private int m_layer_mask;
    public int EnemyLayer { get => m_layer_mask; }

    [Header("유닛의 애니메이터")]
    [SerializeField] private RuntimeAnimatorController m_animator;
    public RuntimeAnimatorController Animator { get => m_animator; }

    [Header("유닛 공통 스탯 정보")]
    [Header("유닛의 체력")]
    [SerializeField] private int m_unit_hp;
    public int HP { get => m_unit_hp; }

    [Header("유닛의 공격력")]
    [SerializeField] private int m_unit_atk;
    public int ATK { get => m_unit_atk; }

    [Header("유닛의 이동 속도")]
    [SerializeField] private float m_unit_spd;
    public float SPD { get => m_unit_spd; }

    [Header("유닛의 사거리")]
    [SerializeField] private float m_atk_range;
    public float Range { get => m_atk_range; }

    [Header("유닛의 공격 쿨타임")]
    [SerializeField] private float m_atk_cool;
    public float ATKCool { get => m_atk_cool; }
}