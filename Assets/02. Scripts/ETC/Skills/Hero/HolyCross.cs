using System.Collections;
using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class HolyCross : Skill
{
    #region Variables
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    private float m_speed;
    private Coroutine m_return_coroutine;

    private int m_elastic_count;
    #endregion Variables

    #region Properties
    public float SPD
    {
        get => m_speed;
        set => m_speed = value;
    }
    #endregion Properties

    private void OnEnable()
    {
        if (m_return_coroutine != null)
        {
            StopCoroutine(m_return_coroutine);
            m_return_coroutine = null;
        }

        m_return_coroutine = StartCoroutine(Co_Return());
    }

    public void Initialize(int atk, 
                           float speed)
    {
        ATK = atk;
        SPD = speed;

        Animator.speed = 1f;
        m_elastic_count = 0;

        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        Rigidbody.linearVelocity = Vector3.right * SPD;
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

        if (collision.gameObject.layer == Layer)
        {
            CreateDamageIndicator(collision.transform.position);

            m_elastic_count++;

            collision.GetComponent<BaseUnit>().Health.UpdateHP(-ATK);

            if (m_elastic_count >= 5)
            {
                Return();
            }
        }
    }
}