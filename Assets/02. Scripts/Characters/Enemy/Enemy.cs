using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Create Enemy")]
public class Enemy : ScriptableObject
{
    [Header("몬스터의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID { get => m_id; }

    [Header("몬스터의 타입")]
    [SerializeField] private EnemyType m_type;
    public EnemyType Type { get => m_type; }

    [Header("몬스터의 체력")]
    [SerializeField] private float m_hp;
    public float HP { get => m_hp; }

    [Header("몬스터의 공격력")]
    [SerializeField] private float m_atk;
    public float ATK { get => m_atk; }

    [Header("몬스터의 사거리")]
    [SerializeField] private float m_range;
    public float Range { get => m_range; }

    [Header("몬스터의 공격 대기 시간")]
    [SerializeField] private float m_interval;
    public float Interval { get => m_interval; }

    [Header("몬스터의 이동 속도")]
    [SerializeField] private float m_speed;
    public float SPD { get => m_speed; }

    [Header("몬스터의 고유한 애니메이터")]
    [SerializeField] private RuntimeAnimatorController m_animator;
    public RuntimeAnimatorController Animator { get => m_animator; }
}
