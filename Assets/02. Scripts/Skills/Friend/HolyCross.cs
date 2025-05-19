using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class HolyCross : Skill
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    private float m_speed;
    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }

    private float m_origin_speed;
    private Vector2 m_origin_direction;
    private Coroutine m_return_coroutine;

    private int m_elastic_count;

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
        Animator.speed = 1f;
        m_elastic_count = 0;

        MoveTowardsTarget();
    }

    public override void Stop()
    {
        SPD = 0;
        Animator.speed = 0f;
    }

    public override void Resume()
    {
        SPD = m_origin_speed;
        Animator.speed = 1f;

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
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.HOLY_CROSS);
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.HOLY_CROSS);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Return();
        }

        if (collision.CompareTag("Enemy"))
        {
            m_elastic_count++;
            collision.GetComponent<EnemyCtrl>().UpdateHP(-ATK);
            CreateDamageIndicator(collision.transform.position);

            if (m_elastic_count == 5)
            {
                Return();
            }
        }
    }
}
