using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("웨이브에 스폰될 몬스터")]
    [SerializeField] private Enemy m_enemy;
    public Enemy Enemy { get => m_enemy; }

    [Header("스폰 시간")]
    [SerializeField] private float m_spawn_time;
    public float SpawnTime { get => m_spawn_time; }

    [Header("스폰될 몬스터의 수")]
    [SerializeField] private int m_count;
    public int Count { get => m_count; }

    [Header("마리 간 간격(초 단위)")]
    [SerializeField] private float m_interval;
    public float Interval { get => m_interval; }
}