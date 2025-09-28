using System.Collections;
using ObjectPool;
using UnityEngine;

public class UnitDeadState : MonoBehaviour, IState<BaseUnit>
{
    private BaseUnit m_unit;

    private readonly float m_return_time = 2.5f;
    private Coroutine m_return_coroutine;

    private void OnEnable()
    {
        if(m_unit == null)
        {
            return;
        }

        m_unit.Rigidbody.simulated = true;
        m_unit.Collider.enabled = true;
        m_unit.Renderer.sortingOrder = 10 + (int)m_unit.Unit.Code;
        
        m_unit.Health.Dead = false;
    }

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
        if(m_return_coroutine != null)
        {
            StopCoroutine(m_return_coroutine);
            m_return_coroutine = null;
        }
    }

    private void Initialize()
    {
        m_unit.Rigidbody.simulated = false;
        m_unit.Collider.enabled = false;
        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Animator.SetTrigger("Death");
        m_unit.Renderer.sortingOrder = 5;

        m_unit.Health.Dead = true;

        m_return_coroutine = StartCoroutine(Co_Return());
    }

    private IEnumerator Co_Return()
    {
        yield return new WaitForSeconds(m_return_time);

        var unit_type = m_unit.Unit.Type;
        var object_type = GetObjectType(unit_type);
        
        var container = ObjectManager.Instance.GetPool(object_type).Container;
        transform.position = container.transform.position;

        ObjectManager.Instance.ReturnObject(gameObject, object_type);
    }

    private ObjectType GetObjectType(UnitType unit_type)
    {
        return unit_type switch
        {
            UnitType.MELEE      => ObjectType.MELEE_UNIT,
            UnitType.RANGED     => ObjectType.RANGED_UNIT,
            UnitType.GUARD      => ObjectType.MELEE_UNIT,
            UnitType.NIMMIA     => ObjectType.NIMMIA,
            UnitType.LELIA      => ObjectType.LELIA,
            _                   => ObjectType.NONE
        };
    }
}