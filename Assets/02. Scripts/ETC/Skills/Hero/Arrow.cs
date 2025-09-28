
using ObjectPool;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : Skill
{
    private float m_speed;
    private bool m_is_returned;

    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }

    public void Initialize(int atk, 
                           float speed, 
                           int layer, 
                           Vector2 direction)
    {
        ATK = atk;
        SPD = speed;

        m_is_returned = false;

        Layer = layer;

        MoveTowardsTarget(direction);
        RotateTowardsDirection(direction);
    }

    public void MoveTowardsTarget(Vector2 direction)
    {
        Rigidbody.linearVelocity = direction * SPD;
    }

    private void RotateTowardsDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, 
                                            ObjectType.ARROW);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_is_returned)
        {
            return;
        }
        
        if (collision.gameObject.layer == Layer)
        {
            m_is_returned = true;
            
            CreateDamageIndicator(collision.transform);
            collision.GetComponent<BaseUnit>().Health.UpdateHP(-ATK);

            Return();
        }
    }
}
