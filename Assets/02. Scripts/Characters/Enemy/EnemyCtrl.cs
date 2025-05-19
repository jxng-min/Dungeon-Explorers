using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public abstract class EnemyCtrl : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public Animator Animator { get; protected set; }

    protected Enemy m_scriptable_object;
    public Enemy Script
    {
        get => m_scriptable_object;
        set => m_scriptable_object = value;
    }

    protected LayerMask m_enemy_layer;

    protected float m_current_hp;
    protected float m_current_atk;
    protected float m_current_cooltime;
    protected float m_current_spd;
    protected float m_current_radius;

    protected bool m_is_attack;
    protected bool m_is_dead;
    public bool IsDead { get => m_is_dead; }

    protected Coroutine m_attack_coroutine;
    protected Coroutine m_knockback_coroutine;

    private bool m_was_knockbacked;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        m_enemy_layer = LayerMask.GetMask("EXPLORER");
    }

    protected void FixedUpdate()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

        if (!m_is_dead && m_knockback_coroutine == null)
        {
            if (!m_is_attack)
            {
                MoveTowardsTower();
            }

            Attack();
        }
    }

    public virtual void Initialize()
    {
        Rigidbody.simulated = true;

        Animator.ResetTrigger("Death");
        m_is_dead = false;

        Collider.enabled = true;

        Renderer.sortingOrder = 10 + Script.ID;
        Renderer.flipX = true;

        m_current_hp = Script.HP;
        m_current_atk = Script.ATK;
        m_current_cooltime = Script.Interval;
        m_current_spd = Script.SPD;
        m_current_radius = Script.Range;

        m_was_knockbacked = false;

        if (m_attack_coroutine != null)
        {
            StopCoroutine(m_attack_coroutine);
            m_attack_coroutine = null;
        }

        if (m_knockback_coroutine != null)
        {
            StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }
    }

    protected virtual void MoveTowardsTower()
    {
        Animator.SetBool("IsMove", true);
        Rigidbody.linearVelocity = Vector2.left * m_current_spd;
    }

    protected abstract void Attack();

    public void UpdateHP(float amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_current_hp += amount;
        if (!m_was_knockbacked && m_current_hp / Script.HP <= 0.4f)
        {
            m_was_knockbacked = true;
            m_is_attack = false;

            Animator.SetTrigger("Hurt");

            if (m_attack_coroutine != null)
            {
                StopCoroutine(m_attack_coroutine);
                m_attack_coroutine = null;
            }

            KnockBack(new Vector2(1, 1));
        }

        if (m_current_hp <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        if (m_is_dead)
        {
            return;
        }

        m_is_dead = true;

        Rigidbody.linearVelocity = Vector2.zero;
        Rigidbody.simulated = false;

        if (m_attack_coroutine != null)
        {
            StopCoroutine(m_attack_coroutine);
            m_attack_coroutine = null;
        }

        if (m_knockback_coroutine != null)
        {
            StopCoroutine(m_knockback_coroutine);
            m_knockback_coroutine = null;
        }

        Animator.speed = 1f;
        Animator.SetTrigger("Death");

        Renderer.sortingOrder = 9;

        Collider.enabled = false;

        Invoke("Return", 2.5f);
    }    

    protected void Return()
    {
        var enemy = GetComponent<EnemyCtrl>();
        if (enemy != null)
        {
            Destroy(enemy);
        }

        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ENEMY);
    }

    public void KnockBack(Vector2 direction, float amount = 0.4f)
    {
        if (m_knockback_coroutine != null)
        {
            StopCoroutine(m_knockback_coroutine);
        }

        m_knockback_coroutine = StartCoroutine(Co_KnockBack(direction, amount));
    }

    private IEnumerator Co_KnockBack(Vector2 direction, float amount)
    {
        float elapsed_time = 0f;
        float target_time = 0.15f;

        Vector2 kps = direction * (amount / target_time);
        if (kps.magnitude > 0f)
        {
            while (elapsed_time <= target_time)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                Rigidbody.MovePosition(Rigidbody.position + kps * Time.deltaTime);

                yield return null;
            }
        }

        m_knockback_coroutine = null;
    }

    protected void CreateDamageIndicator(Vector3 position)
    {
        var obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        obj.transform.position = position;

        var damage_indicator = obj.GetComponent<DamageIndicator>();
        damage_indicator.Initialize($"<color=#F6BB43>{NumberFormatter.FormatNumber(m_current_atk)}</color>");
    }
}
