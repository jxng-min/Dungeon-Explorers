using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Meteor : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    private int m_atk;
    public int ATK
    {
        get => m_atk;
        set => m_atk = value;
    }

    private float m_speed;
    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }

    private float m_origin_speed;
    private Vector2 m_origin_direction;

    public void Initialize(int atk, float spd)
    {
        ATK = atk;
        SPD = spd;
        m_origin_speed = spd;

        Animator.SetFloat("Color", Random.Range(0, 4));

        Move();
        RotateTowardsDirection(m_origin_direction);
    }

    public void Stop()
    {
        SPD = 0;
    }

    public void Resume()
    {
        SPD = m_origin_speed;

        Move(m_origin_direction);
    }

    private void Move()
    {
        Vector2 direction = new Vector2(1f, -1.5f).normalized;
        m_origin_direction = direction;

        Move(m_origin_direction);
    }

    public void Move(Vector2 direction)
    {
        Rigidbody.linearVelocity = direction * SPD;
    }

    private void RotateTowardsDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void CreateExplosions()
    {
        var obj = ObjectManager.Instance.GetObject(ObjectType.EXPLOSION);
        obj.transform.position = transform.position;

        var explosion = obj.GetComponent<Explosion>();
        explosion.Animator.SetFloat("Pattern", Random.Range(0, 4));
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyCtrl>().UpdateHP(-ATK);
        }

        if (collision.CompareTag("Ground"))
        {
            CreateExplosions();
            ObjectManager.Instance.ReturnObject(gameObject, ObjectType.METEOR);
        }
    }
}
