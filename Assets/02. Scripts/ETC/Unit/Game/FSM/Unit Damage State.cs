using System.Collections;
using UnityEngine;

public class UnitDamageState : MonoBehaviour, IState<BaseUnit>
{
    private BaseUnit m_unit;

    private readonly float m_knockback_power = 0.75f;
    private Vector2 m_knockback_velocity;
    private Coroutine m_knockback_coroutine;

    private void OnEnable()
    {
        if(m_unit == null)
        {
            return;
        }

        m_unit.Health.KnockBack = false;
    }

    private void OnDisable()
    {
        if(m_unit == null)
        {
            return;
        }

        m_knockback_velocity = Vector2.zero;

        ExecuteExit();
    }

    public void ExecuteEnter(BaseUnit sender)
    {
        if(m_unit == null)
        {
            m_unit = sender;
        }

        Initialize();
    }

    public void ExecuteExit()
    {
        if(m_knockback_coroutine != null)
        {
            StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }
    }

    private Vector2 CalculateVelocity(float power) => IsHero() ? power * Vector2.left :
                                                                 power * Vector2.right;

    private void Initialize()
    {
        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Animator.SetTrigger("Hurt");

        m_unit.Health.KnockBack = true;

        m_knockback_velocity = CalculateVelocity(m_knockback_power);
        m_knockback_coroutine = StartCoroutine(Co_Knockback());
    }

    private bool IsHero()
    {
        var target_enemy_layer = m_unit.Unit.EnemyLayer;
        var enemy_layer = LayerMask.NameToLayer("ENEMY");

        return target_enemy_layer == enemy_layer;
    }

    private IEnumerator Co_Knockback()
    {
        float elapsed_time = 0f;
        float target_time = 0.5f;

        var current_position = m_unit.Rigidbody.position;

        var kps = m_knockback_velocity * (m_knockback_power / target_time);
        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            current_position += kps * Time.deltaTime;

            m_unit.Rigidbody.MovePosition(current_position);

            yield return null;
        }

        m_unit.ChangeState(UnitState.MOVE);
    }
}
