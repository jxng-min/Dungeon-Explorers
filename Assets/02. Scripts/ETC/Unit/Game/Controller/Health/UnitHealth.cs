using System;
using InventoryService;
using Units;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class UnitHealth : MonoBehaviour, IHealth
{
    private BaseUnit m_unit;
    
    private IInventoryService m_inventory_service;

    public event Action OnDead;

    private int m_max_hp;

    public int HP { get; set; }
    public bool KnockBack { get; set; }
    public bool Dead { get; set; }

    public void Awake()
    {
        m_unit = GetComponent<BaseUnit>();
    }

    public void Initialize(int hp)
    {
        m_inventory_service = ServiceLocator.Get<IInventoryService>();

        HP = !IsHero() ? m_unit.Unit.HP :
                         m_unit.Unit.HP + (m_unit.Unit as Hero).GrowthHP * (m_inventory_service.GetUnit(m_unit.Unit.Code).Upgrade - 1);
        
        m_max_hp = HP;
    }

    public void UpdateHP(int amount)
    {
        if (Dead)
        {
            return;
        }

        HP += amount;

        if (HP <= 0f)
        {
            m_unit.ChangeState(UnitState.DEATH);
            OnDead?.Invoke();
            return;
        }

        if (!KnockBack && ((float)HP / m_max_hp) <= 0.4f)
        {
            m_unit.ChangeState(UnitState.DAMAGE);
            return;
        }
    }

    private bool IsHero()
    {
        var target_enemy_layer = m_unit.Unit.EnemyLayer;
        var enemy_layer = LayerMask.NameToLayer("ENEMY");

        return target_enemy_layer == enemy_layer;
    }
}