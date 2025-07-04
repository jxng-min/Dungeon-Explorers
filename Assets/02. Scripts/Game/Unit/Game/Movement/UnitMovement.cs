using UnityEngine;

public class UnitMovement : IMovement
{
    #region Variables
    private BaseUnit m_unit;
    private float m_current_speed;
    #endregion Variables

    #region Properties
    public float SPD { get => m_current_speed; }
    #endregion Properties

    public UnitMovement(BaseUnit unit, float speed = 0f)
    {
        m_unit = unit;
        m_current_speed = speed;
    }

    #region Helper Methods
    public void Initialize(float speed)
    {
        m_current_speed = speed;
    }

    public virtual void Move()
    {
        m_unit.Animator.SetBool("IsMove", true);

        m_unit.Rigidbody.linearVelocity = m_unit.Unit.EnemyLayer == LayerMask.NameToLayer("ENEMY")
            ? Vector2.right * m_current_speed : Vector2.left * m_current_speed; 
    }
    #endregion Helper Methods
}