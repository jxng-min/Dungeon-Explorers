using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class HolyShield : Skill
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Initialize(float atk, Vector3 position)
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
        transform.position = target_position + Vector3.up * 0.25f;
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f, Layer);

        foreach (var hit in hits)
        {
            CreateDamageIndicator(hit.transform.position);
        }
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.HOLY_SHIELD);
    }
}
