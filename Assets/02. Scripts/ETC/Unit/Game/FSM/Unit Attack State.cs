using UnityEngine;

public class UnitAttackState : MonoBehaviour, IState<BaseUnit>
{
    protected BaseUnit m_unit;

    public virtual void ExecuteEnter(BaseUnit sender)
    {
        if(m_unit == null)
        {
            m_unit = sender;
        }
    }

    public virtual void ExecuteExit() {}

    protected bool IsHero()
    {
        var target_enemy_layer = m_unit.Unit.EnemyLayer;
        var enemy_layer = LayerMask.NameToLayer("ENEMY");

        return target_enemy_layer == enemy_layer;
    }
}