using System.Collections;
using UnityEngine;

public class UnitMoveState : MonoBehaviour, IState<BaseUnit>
{
    private BaseUnit m_unit;

    private Vector2 m_velocity;
    private Coroutine m_move_coroutine;

    private void OnDisable()
    {
        if(m_unit == null)
        {
            return;
        }

        m_velocity = Vector2.zero;

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
        if(m_move_coroutine != null)
        {
            StopCoroutine(m_move_coroutine);
            m_move_coroutine = null;
        }
    }

    private void Initialize()
    {
        m_velocity = CalculateVelocity(m_unit.Unit.SPD);

        m_unit.Animator.SetBool("Move", true);
        m_move_coroutine = StartCoroutine(Co_Move());
    }

    private Vector2 CalculateVelocity(float speed) => IsHero() ? speed * Vector2.right :
                                                                 speed * Vector2.left; 

    private IEnumerator Co_Move()
    {
        while(true)
        {
            if(m_unit.Attack.CanAttack())
            {
                m_unit.ChangeState(UnitState.ATTACK);
                yield break;
            }
            else
            {
                m_unit.Rigidbody.linearVelocity = m_velocity;
            }
            
            yield return null;
        }
    }

    private bool IsHero()
    {
        var target_enemy_layer = m_unit.Unit.EnemyLayer;
        var enemy_layer = LayerMask.NameToLayer("ENEMY");
        
        return target_enemy_layer == enemy_layer;
    }
}
