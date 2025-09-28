using System.Collections;
using UnityEngine;

public class UnitIdleState : MonoBehaviour, IState<BaseUnit>
{
    private BaseUnit m_unit;
    private Coroutine m_idle_coroutine;

    private void OnDisable()
    {
        if(m_unit == null)
        {
            return;
        }

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
        if(m_idle_coroutine != null)
        {
            StopCoroutine(m_idle_coroutine);
            m_idle_coroutine = null;
        }
    }

    private void Initialize()
    {
        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Animator.SetBool("Move", false);

        m_idle_coroutine = StartCoroutine(Co_Idle(m_unit.Attack.Interval));
    }

    private IEnumerator Co_Idle(float idle_time)
    {
        float elapsed_time = 0f;
        
        while(elapsed_time < idle_time)
        {
            elapsed_time += Time.deltaTime;

            if(!m_unit.Attack.CanAttack())
            {
                m_unit.ChangeState(UnitState.MOVE);
                yield break;
            }

            yield return null;
        }

        m_unit.ChangeState(UnitState.MOVE);
    }
}
