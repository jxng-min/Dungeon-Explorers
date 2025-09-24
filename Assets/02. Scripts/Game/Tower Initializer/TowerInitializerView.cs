using UnityEngine;

public class TowerInitializerView : MonoBehaviour, ITowerInitializerView
{
    [Header("아군 타워")]
    [SerializeField] private TowerUnit m_hero_tower_unit;

    [Header("적군 타워")]
    [SerializeField] private TowerUnit m_enemy_tower_unit;


    public void InitTower(bool is_hero, int hp)
    {
        if(is_hero)
        {
            m_hero_tower_unit.Initialize(is_hero, hp);
        }
        else
        {
            m_enemy_tower_unit.Initialize(is_hero, hp);
        }
    }
}
