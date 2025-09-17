using Units;
using UnityEngine;

[System.Serializable]
public class TrainerData
{
    [Header("영웅 유닛")]
    [SerializeField] private Hero m_hero_unit;
    public Hero Hero => m_hero_unit;

    [Header("기본 강화 비용")]
    [SerializeField] private int m_default_cost;
    public int DefaultCost => m_default_cost;

    [Header("성장 강화 비용")]
    [SerializeField] private int m_growth_cost;
    public int GrowthCost => m_growth_cost;

    [Header("최대 강화 횟수")]
    [SerializeField] private int m_train_limit;
    public int Limit => m_train_limit;
}
