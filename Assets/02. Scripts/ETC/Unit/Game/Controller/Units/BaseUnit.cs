using UnityEngine;

[RequireComponent(typeof(IAttack), typeof(IHealth))]
public abstract class BaseUnit : MonoBehaviour
{
    #region Unit States
    protected UnitStateContext m_state_context;

    protected IState<BaseUnit> m_return_state;
    protected IState<BaseUnit> m_idle_state;
    protected IState<BaseUnit> m_move_state;
    protected IState<BaseUnit> m_attack_state;
    protected IState<BaseUnit> m_damage_state;
    protected IState<BaseUnit> m_dead_state;
    #endregion Unit States

    private bool m_is_inited;

    #region Properties
    public Unit Unit { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public SpriteRenderer Renderer { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public CircleCollider2D Range { get; protected set; }
    public Animator Animator { get; protected set; }

    public IHealth Health { get; protected set; }
    public IAttack Attack { get; protected set; }
    #endregion Properties

    protected virtual void Awake()
    {
        m_state_context = new UnitStateContext(this);

        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        Health = GetComponent<IHealth>();
        Attack = GetComponent<IAttack>();

        m_idle_state = gameObject.AddComponent<UnitIdleState>();
        m_move_state = gameObject.AddComponent<UnitMoveState>();
        m_damage_state = gameObject.AddComponent<UnitDamageState>();
        m_dead_state = gameObject.AddComponent<UnitDeadState>();
    }

    private void OnDisable()
    {
        m_is_inited = false;
    }

    private void OnDrawGizmos()
    {
        if (!m_is_inited)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Unit.Range);
    }

    public virtual void Initialize(Unit unit)
    {
        Unit = unit;

        Animator.runtimeAnimatorController = Unit.Animator;

        var is_enemy = Unit.EnemyLayer != LayerMask.NameToLayer("ENEMY");
        Renderer.flipX = is_enemy;

        gameObject.layer = is_enemy ? LayerMask.NameToLayer("ENEMY") :
                                      LayerMask.NameToLayer("HERO"); 

        Health.Initialize();
        Attack.Initialize();

        ChangeState(UnitState.MOVE);
    }

    public void ChangeState(UnitState state)
    {
        Debug.Log($"{Unit.Code}: {state}");
        var new_state = state switch
        {
            UnitState.IDLE      => m_idle_state,
            UnitState.MOVE      => m_move_state,
            UnitState.ATTACK    => m_attack_state,
            UnitState.DAMAGE    => m_damage_state,
            UnitState.DEATH     => m_dead_state,
            _                   => m_return_state
        };

        m_state_context.Transition(new_state);
    }
}
