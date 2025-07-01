using System.Collections;
using ObjectPool;
using UnityEngine;

public class UnitHealth : IHealth
{
    #region Variables
    private BaseUnit m_unit;

    private float m_current_hp;
    private bool m_is_dead;
    private bool m_can_knockback;

    private Coroutine m_knockback_coroutine;
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
    }

    public void UpdateHP(int amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_current_hp += amount;
        if (m_can_knockback && m_current_hp / m_unit.Unit.HP <= 0.4f)
        {
            m_can_knockback = false;

            m_unit.Animator.SetTrigger("Hurt");

            if (m_unit.Unit.EnemyLayer == 6)
            {
                m_knockback_coroutine = m_unit.StartCoroutine(Co_Knockback(new Vector2(1, 1)));
            }
            else
            {
                m_knockback_coroutine = m_unit.StartCoroutine(Co_Knockback(new Vector2(-1, 1)));
            }
        }

        if (m_current_hp <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        if (m_is_dead)
        {
            return;
        }
        m_is_dead = true;

        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Rigidbody.simulated = false;

        if (m_knockback_coroutine != null)
        {
            m_unit.StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }

        if (m_unit.Attack.AttackCoroutine != null)
        {
            m_unit.StopCoroutine(m_unit.Attack.AttackCoroutine);
            m_unit.Attack.AttackCoroutine = null;
        }

        m_unit.Renderer.sortingOrder = 9;
        m_unit.Animator.SetTrigger("Death");

        m_unit.Collider.enabled = false;

        m_unit.StartCoroutine(Co_ReturnUnit(2.5f));
    }
    #endregion Helper Methods

    #region Coroutines
    public IEnumerator Co_Knockback(Vector2 direction, float amount = 0.4f)
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
        ObjectManager.Instance.ReturnObject(m_unit.gameObject, ObjectType.MELEE_UNIT);
    }
    #endregion Coroutines
}
