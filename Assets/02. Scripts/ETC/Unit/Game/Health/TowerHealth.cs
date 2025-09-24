using System;
using System.Collections;
using UnityEngine;

public class TowerHealth : IHealth
{
    #region Variables
    private BaseUnit m_unit;
    private float m_current_hp;
    private float m_max_hp;
    private bool m_is_dead;
    #endregion Variables

    #region Properties
    public float HP { get => m_current_hp; }
    public bool IsDead { get => m_is_dead; }
    public Coroutine KnockBackCoroutine { get; set; }
    #endregion Properties

    public event Action OnDead;

    public TowerHealth(BaseUnit unit)
    {
        m_unit = unit;

        m_current_hp = 0f;
        m_is_dead = false;
    }

    #region Helper Methods
    public void Death()
    {
        if (m_is_dead)
        {
            return;
        }
        
        m_is_dead = true;

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

    public void Initialize(float hp = 0)
    {
        m_max_hp = hp;
        UpdateHP((int)m_max_hp);
    }

    public void UpdateHP(int amount)
    {
        m_current_hp += amount;
        m_current_hp = Mathf.Clamp(m_current_hp, 0, m_max_hp);

        (m_unit as TowerUnit).TowerView.UpdateUI(m_current_hp, m_max_hp);

        if (m_current_hp <= 0f)
        {
            Death();
        }
    }

    public IEnumerator Co_Knockback(Vector2 direction, float amount = 0.4F) { yield break; }

    public IEnumerator Co_ReturnUnit(float target_time) { yield break; }
    #endregion Helper Methods
}
