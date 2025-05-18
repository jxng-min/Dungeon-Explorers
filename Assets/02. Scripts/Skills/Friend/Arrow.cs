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
    private Coroutine m_return_coroutine;

    private void OnEnable()
    {
        if (m_return_coroutine != null)
        {
            StopCoroutine(m_return_coroutine);
            m_return_coroutine = null;
        }

        m_return_coroutine = StartCoroutine(Co_Return());
    }

    public void Initialize(float atk, float speed)
    {
        ATK = atk;
        SPD = speed;
        m_origin_speed = speed;

        MoveTowardsTarget();
        RotateTowardsDirection(m_origin_direction);
    }

    public override void Stop()
    {
        SPD = 0;
    }

    public override void Resume()
    {
        SPD = m_origin_speed;

        MoveTowardsTarget(m_origin_direction);
    }

    public void MoveTowardsTarget()
    {
        ;
        m_origin_direction = Vector2.right;

        MoveTowardsTarget(Vector2.right);
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

    private IEnumerator Co_Return()
    {
        float elapsed_time = 0f;
        float target_time = 5f;

        while (elapsed_time <= target_time)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;
            yield return null;
        }

        m_return_coroutine = null;
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ARROW);
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ARROW);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CreateDamageIndicator(collision.transform.position);
            // TODO: 데미지를 입히는 로직
            Invoke("Return", 0.1f);
        }
    }
}
