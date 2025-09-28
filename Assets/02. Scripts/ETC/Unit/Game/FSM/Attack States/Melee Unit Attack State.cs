using UnityEngine;

public class MeleeUnitAttackState : UnitAttackState
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

    public void AttackToTarget()
    {
        var target = m_unit.Attack.GetTarget();
        
        if(target != null)
        {
            var target_unit = target.GetComponent<BaseUnit>();
            target_unit.Health.UpdateHP(-m_unit.Attack.ATK);

            m_unit.Attack.CreateDamageIndicator(target_unit.transform);
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
}