using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Meteor : Skill
{
    private float m_speed;
    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }

    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    public void Initialize(int atk, float spd)
    {
        ATK = atk;
        SPD = spd;

        Animator.SetFloat("Color", Random.Range(0, 4));

        Move();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(1f, -1.5f).normalized;
        RotateTowardsDirection(direction);

        Rigidbody.linearVelocity = direction * SPD;
    }

    private void RotateTowardsDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layer)
        {
            collision.GetComponent<BaseUnit>().Health.UpdateHP(-ATK);
            CreateDamageIndicator(collision.transform);
        }

        if (collision.CompareTag("Ground"))
        {
            Rigidbody.linearVelocity = Vector3.zero;
            Animator.SetTrigger("Explosion");
            Animator.SetFloat("Pattern", Random.Range(0, 4));
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.METEOR);
    }
}
