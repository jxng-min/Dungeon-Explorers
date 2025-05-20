using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : Skill
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

    private float m_speed;
    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }

    private float m_origin_speed;
    private Vector2 m_origin_direction;

    public void Initialize(int atk, float speed, LayerMask layer, Vector2 direction)
    {
        ATK = atk;
        SPD = speed;
        m_origin_speed = speed;

        Layer = layer;

        MoveTowardsTarget(direction);
        RotateTowardsDirection(m_origin_direction);
    }

    public override void Stop()
    {
        SPD = 0;
        Rigidbody.linearVelocity = Vector2.zero;
        Debug.Log(Rigidbody.linearVelocity);
    }

    public override void Resume()
    {
        SPD = m_origin_speed;

        MoveTowardsTarget(m_origin_direction);
    }

    public void MoveTowardsTarget(Vector2 direction)
    {
        m_origin_direction = direction;

        Rigidbody.linearVelocity = direction * SPD;
    }

    private void RotateTowardsDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ARROW);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & Layer) != 0)
        {
            CreateDamageIndicator(collision.transform.position);

            if (Layer == LayerMask.GetMask("ENEMY"))
            {
                collision.GetComponent<EnemyCtrl>().UpdateHP(-ATK);
            }
            else
            {
                collision.GetComponent<Character>().UpdateHP(-ATK);
            }
            
            Invoke("Return", 0.1f);
        }
    }
}
