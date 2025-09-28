using ObjectPool;
using UnityEngine;

public class RangedUnitAttackState : UnitAttackState
{
    public override void ExecuteEnter(BaseUnit sender)
    {
        base.ExecuteEnter(sender);

        Initialize();    
    }

    private void Initialize()
    {
        m_unit.Rigidbody.linearVelocity = Vector2.zero;

        m_unit.Animator.SetBool("Move", false);
        m_unit.Animator.SetTrigger("Attack");
    }

    public void ShotToTarget()
    {
        var target = m_unit.Attack.GetTarget();
        
        if(target != null)
        {
            InstantiateArrow();
        }
    }

    public void ChangeState()
    {
        if(m_unit.Health.Dead)
        {
            return;
        }

        if(m_unit.Attack.CanAttack())
        {
            m_unit.ChangeState(UnitState.IDLE);
        }
        else
        {
            m_unit.ChangeState(UnitState.MOVE);
        }
    }

    private void InstantiateArrow()
    {
        var arrow_obj = ObjectManager.Instance.GetObject(ObjectType.ARROW);
        arrow_obj.transform.position = transform.position + 0.25f * Vector3.up;

        var arrow = arrow_obj.GetComponent<Arrow>();
        arrow.Initialize(m_unit.Attack.ATK,
                         8f,
                         m_unit.Unit.EnemyLayer,
                         IsHero() ? Vector2.right : Vector2.left);
    }
}
