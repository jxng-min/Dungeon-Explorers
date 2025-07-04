using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class HolyShield : Skill
{
    #region Variables
    private Animator m_animator;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    #region Helper Methods
    public void Initialize(int atk, Vector3 position)
    {
        ATK = atk;

        SetPosition(position);
    }

    public override void Stop()
    {
        m_animator.speed = 0f;
    }

    public override void Resume()
    {
        m_animator.speed = 1f;
    }

    private void SetPosition(Vector3 target_position)
    {
        transform.position = target_position + Vector3.up * 1.1f;
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f, Layer);

        foreach (var hit in hits)
        {
            CreateDamageIndicator(hit.transform.position);

            hit.GetComponent<BaseUnit>().Health.UpdateHP(-ATK);
        }
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.HOLY_SHIELD);
    }
    #endregion Helper Methods
}
