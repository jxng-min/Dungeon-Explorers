using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "SO/Create Hero")]
    public class Hero : Unit
    {
        [Space(50f)]
        [Header("영웅 스탯 정보 관련")]
        [Header("영웅의 성장 체력")]
        [SerializeField] private float m_growth_hp;
        public float GrowthHP { get => m_growth_hp; }

        [Header("영웅의 성장 공격력")]
        [SerializeField] private int m_growth_atk;
        public int GrowthATK { get => m_growth_atk; }

        [Header("영웅의 소환 비용")]
        [SerializeField] private int m_cost;
        public int Cost { get => m_cost; }

        [Header("영웅의 소환 대기 시간")]
        [SerializeField] private float m_cooltime;
        public float SpawnCool { get => m_cooltime; }
    }
}