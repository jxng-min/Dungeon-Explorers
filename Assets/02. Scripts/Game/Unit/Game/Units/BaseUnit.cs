using UnityEngine;
using Units;
using ObjectPool;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public abstract class BaseUnit : MonoBehaviour
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 스크립터블 오브젝트")]
    [SerializeField] private Unit m_unit;

    protected IMovement m_movement;
    protected IHealth m_health;
    protected IAttack m_attack;
    #endregion Variables

    #region Properties
    public Unit Unit { get => m_unit; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public SpriteRenderer Renderer { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public Animator Animator { get; protected set; }

    public IMovement Movement { get => m_movement; }
    public IHealth Health { get => m_health; }
    public IAttack Attack { get => m_attack; }
    #endregion Properties

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        m_movement = new UnitMovement(this);
        m_health = new UnitHealth(this);
        m_attack = null;
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

        if (!m_health.IsDead && m_health.KnockBackCoroutine == null)
        {
            if (!m_attack.IsAttack)
            {
                m_movement.Move();
            }

            m_attack.Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_attack.Range);
    }

    #region Helper Methods
    public void Initialize(Unit unit)
    {
        m_unit = unit;

        Animator.runtimeAnimatorController = unit.Animator;

        m_movement.Initialize(unit.SPD);
        m_health.Initialize(unit.HP);
        m_attack.Initialize(unit.EnemyLayer, unit.ATK, unit.ATKCool, unit.Range);
    }

    public void CreateDamageIndicator(Vector2 position)
    {
        var obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        obj.transform.position = position + Vector2.up * 0.5f;

        var damage_indicator = obj.GetComponent<DamageIndicator>();
        damage_indicator.Initialize($"<color=#F6BB43>{NumberFormatter.FormatNumber(m_attack.ATK)}</color>");
    }
    #endregion Helper Methods
}
