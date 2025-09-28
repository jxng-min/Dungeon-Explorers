using Units;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    [Header("영웅 유닛")]
    [SerializeField] private Hero m_hero;
    public Hero Hero => m_hero;

    [Header("구매가")]
    [SerializeField] private int m_cost;
    public int Cost => m_cost;
}