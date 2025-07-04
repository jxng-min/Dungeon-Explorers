using System.Collections;
using ObjectPool;
using UnityEngine;

public class RangedAttack : IAttack
{
    #region Variables
    private BaseUnit m_unit;

    private int m_current_atk;
    private float m_current_cooltime;
    private float m_current_range;
    private bool m_is_attack;
    private LayerMask m_enemy_layer;

    private Coroutine m_attack_coroutine;
    #endregion Variables

    #region Properties
    public int ATK { get => m_current_atk; }
    public float ATKCool { get => m_current_cooltime; }
    public float Range { get => m_current_range; }
    public bool IsAttack { get => m_is_attack; }

    public Coroutine AttackCoroutine
    {
        get => m_attack_coroutine;
        set => m_attack_coroutine = value;
    }
    #endregion Properties

    public RangedAttack(BaseUnit unit)
    {
        m_unit = unit;
    }

    #region Helper Methods
    public void Initialize(int enemy_layer, int atk, float cool_time, float range)
    {
        m_current_atk = atk;
        m_current_cooltime = cool_time;
        m_current_range = range;
        m_enemy_layer = enemy_layer;

        m_unit.gameObject.layer = (enemy_layer == LayerMask.NameToLayer("ENEMY"))
                                    ? LayerMask.NameToLayer("HERO") : LayerMask.NameToLayer("ENEMY");
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(m_unit.transform.position, m_current_range, m_enemy_layer);
        if (hits.Length == 0)
        {
            m_is_attack = false;
            return;
        }

        m_is_attack = true;
        m_unit.Rigidbody.linearVelocity = Vector2.zero;
        m_unit.Animator.SetBool("IsMove", false);

        Collider2D closest = null;
        var min_distance = float.MaxValue;
        foreach (var hit in hits)
        {
            var distance = Vector2.Distance(m_unit.transform.position, hit.transform.position);
            if (distance < min_distance)
            {
                closest = hit;
                min_distance = distance;
            }
        }

        if (closest != null)
        {
            if (m_attack_coroutine != null)
            {
                return;
            }

            m_attack_coroutine = m_unit.StartCoroutine(Co_Attack(closest.gameObject));
        }
    }

    public IEnumerator Co_Attack(GameObject obj)
    {
        var unit = obj.GetComponent<BaseUnit>();
        if (!unit)
        {
            yield break;
        }

        float elapsed_time = 0f;
        while (!unit.Health.IsDead)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            if (!unit.Health.IsDead)
            {
                m_unit.Animator.SetTrigger("Attack");
                Action(unit, 0.8f);
            }

            elapsed_time = 0f;
        }

        m_is_attack = false;
        m_attack_coroutine = null;
    }

    public void Action(BaseUnit unit, float delay)
    {
        m_unit.Invoke("CreateArrow", 0.8f);
    }

    public void CreateArrow(BaseUnit unit)
    {
        if (!m_is_attack)
        {
            return;
        }

        var arrow_obj = ObjectManager.Instance.GetObject(ObjectType.ARROW);
        arrow_obj.transform.position = m_unit.transform.position + Vector3.up * 0.25f;

        var target_direction = (unit.transform.position - m_unit.transform.position).normalized;

        var arrow = arrow_obj.GetComponent<Arrow>();
        arrow.Initialize(m_current_atk, 8f, m_enemy_layer, target_direction);
    }
    #endregion Helper Methods
}