using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class HolyShield : Skill
{
    public void Initialize(int atk, Vector3 position)
    {
        ATK = atk;

        SetPosition(position);
    }

    private void SetPosition(Vector3 target_position)
    {
        transform.position = target_position + Vector3.up * 1.1f;
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f, 1 << Layer);

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
}
