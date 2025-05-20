using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Scriptable Object/Create Stage")]
public class Stage : ScriptableObject
{
    [Header("스테이지의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID { get => m_id; }
    
    [Header("적군 타워 체력")]
    [SerializeField] private int m_enemy_base_hp;
    public int BaseHP { get => m_enemy_base_hp; }

    [Header("적 웨이브의 목록")]
    [SerializeField] private Wave[] m_enemy_waves;
    public Wave[] Waves { get => m_enemy_waves; }

    [Header("보상으로 획득할 골드")]
    [SerializeField] private int m_reward_gold;
    public int Gold { get => m_reward_gold; }

    [Header("보상으로 획득할 경험치")]
    [SerializeField] private int m_reward_exp;
    public int EXP { get => m_reward_exp; }
}
