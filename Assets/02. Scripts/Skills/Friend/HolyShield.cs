using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class HolyShield : MonoBehaviour
{
    [Header("적들의 레이어")]
    [SerializeField] private LayerMask m_enemy_layer;

    private float m_atk;
    public float ATK
    {
        get => m_atk;
        set => m_atk = value;
    }

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

    public void Stop()
    {
        m_animator.speed = 0f;
    }

    public void Resume()
    {
        m_animator.speed = 1f;
    }

    private void SetPosition(Vector3 target_position)
    {
        transform.position = target_position + Vector3.up * 0.25f;
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f, m_enemy_layer);

        // foreach (var hit in hits)
        // {
        //     Enemy enemy = hit.GetComponent<Enemy>();
        //     if (enemy != null)
        //     {
        //         enemy.UpdateHP(-ATK); // 피해 적용
        //     }
        // }        
    }

    public void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.HOLY_SHIELD);
    }
}
