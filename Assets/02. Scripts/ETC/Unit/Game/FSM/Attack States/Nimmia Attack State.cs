using ObjectPool;
using UnityEngine;

public class NimmiaAttackState : UnitAttackState
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

    public void MagicToTarget()
    {
        var target = m_unit.Attack.GetTarget();
        
        if(target != null)
        {
            InstantiateCross();
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

    private void InstantiateCross()
    {
        var cross_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_CROSS);
        cross_obj.transform.position = transform.position + 0.5f * Vector3.up;

        var cross = cross_obj.GetComponent<HolyCross>();
        cross.Initialize(m_unit.Attack.ATK, 8f);
    }
}
