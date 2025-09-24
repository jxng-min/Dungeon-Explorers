using System;
using System.Collections;
using ObjectPool;
using UnityEngine;

public class UnitHealth : IHealth
{
    #region Variables
    private BaseUnit m_unit;

    private float m_current_hp;
    private bool m_is_dead;
    private bool m_can_knockback = true;

    private Coroutine m_knockback_coroutine;

    public event Action OnDead;
    #endregion Variables

    #region Properties
    public float HP { get => m_current_hp; }
    public bool IsDead { get => m_is_dead; }

    public Coroutine KnockBackCoroutine
    {
        get => m_knockback_coroutine;
        set => m_knockback_coroutine = value;
    }
    #endregion Properties

    public UnitHealth(BaseUnit unit, float hp = 0f)
    {
        m_unit = unit;
        m_current_hp = hp;
    }

    #region Helper Methods
    public void Initialize(float hp)
    {
        m_current_hp = hp;

        m_is_dead = false;
        m_can_knockback = true;

        if (m_knockback_coroutine != null)
        {
            m_unit.StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }
    }

    public void UpdateHP(int amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_current_hp += amount;
        if (m_can_knockback && (m_current_hp / m_unit.Unit.HP) <= 0.4f)
        {
            m_can_knockback = false;

            m_unit.Animator.SetTrigger("Hurt");

            if (m_unit.Unit.EnemyLayer == LayerMask.NameToLayer("ENEMY"))
            {
                m_knockback_coroutine = m_unit.StartCoroutine(Co_Knockback(new Vector2(-1, 1)));
            }
            else
            {
                m_knockback_coroutine = m_unit.StartCoroutine(Co_Knockback(new Vector2(1, 1)));
            }
        }

        if (m_current_hp <= 0f)
        {
            Death();
            OnDead?.Invoke();
        }
    }

    public void Death()
    {
        if (m_is_dead)
        {
            return;
        }
        m_is_dead = true;

        m_can_knockback = true;

        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Rigidbody.simulated = false;

        if (m_knockback_coroutine != null)
        {
            m_unit.StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }

        m_unit.Attack.ResetAttack();

        m_unit.Renderer.sortingOrder = 9;
        m_unit.Animator.SetTrigger("Death");

        m_unit.Collider.enabled = false;

        m_unit.StartCoroutine(Co_ReturnUnit(2.5f));
    }
    #endregion Helper Methods

    #region Coroutines
    public IEnumerator Co_Knockback(Vector2 direction, float amount = 0.7f)
    {
        float elapsed_time = 0f;
        float target_time = 0.15f;

        Vector2 kps = direction * (amount / target_time);
        if (kps.magnitude > 0f)
        {
            while (elapsed_time <= target_time)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                m_unit.Rigidbody.MovePosition(m_unit.Rigidbody.position + kps * Time.deltaTime);

                yield return null;
            }
        }

        m_knockback_coroutine = null;
    }

    public IEnumerator Co_ReturnUnit(float target_time)
    {
        yield return new WaitForSeconds(target_time);

        m_unit.gameObject.transform.localPosition = Vector3.zero;
        ObjectManager.Instance.ReturnObject(m_unit.gameObject, GetObjectType());
    }

    private ObjectType GetObjectType()
    {
        switch (m_unit.Unit.Type)
        {
            case UnitType.MELEE:
            case UnitType.GUARD:
                return ObjectType.MELEE_UNIT;

            case UnitType.RANGED:
                return ObjectType.RANGED_UNIT;

            case UnitType.NIMMIA:
                return ObjectType.NIMMIA;

            case UnitType.LELIA:
                return ObjectType.LELIA;
        }

        return ObjectType.NONE;
    }
    #endregion Coroutines
}
