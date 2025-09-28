using ObjectPool;
using UnityEngine;

public class LeliaAttackState : UnitAttackState
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


        var target = m_unit.Attack.GetTarget();

        if(target != null)
        {
            var shield_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_SHIELD);
            var shield = shield_obj.GetComponent<HolyShield>();

            shield.Initialize(m_unit.Attack.ATK, target.transform.position);
        }
    }
}
