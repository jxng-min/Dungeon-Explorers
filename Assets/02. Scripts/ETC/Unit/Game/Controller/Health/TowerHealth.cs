using System;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class TowerHealth : MonoBehaviour, IHealth
{
    private BaseUnit m_unit;

    public event Action OnDead;

    public int HP { get; set; }
    public int MaxHP { get; private set; }

    public bool KnockBack { get; set; }
    public bool Dead { get; set; }

    public void Awake()
    {
        m_unit = GetComponent<BaseUnit>();
    }

    public void Initialize(int hp)
    {
        MaxHP = hp;
        UpdateHP(MaxHP);
    }

    public void UpdateHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, MaxHP);

        (m_unit as TowerUnit).TowerView.UpdateUI(HP, MaxHP);

        if (HP <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        if (Dead)
        {
            return;
        }
        
        Dead = true;

        if ((m_unit as TowerUnit).IsHero)
        {
            GameEventBus.Publish(GameEventType.GAMEOVER);
        }
        else
        {
            GameEventBus.Publish(GameEventType.GAMECLEAR);
        }

        OnDead?.Invoke();
    }
}
