using Units;
using UnityEngine;

public class TowerUnit : BaseUnit
{
    #region Variables
    private bool m_is_hero;

    [Header("타워 UI 뷰")]
    [SerializeField] private TowerView m_tower_view;
    #endregion Variables

    #region Properties
    public bool IsHero { get => m_is_hero; }
    public ITowerView TowerView { get => m_tower_view; }
    #endregion Properties

    protected override void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        m_movement = null;
        m_health = new TowerHealth(this);
        m_attack = null;
    }

    protected override void FixedUpdate() { }

    #region Helper Methods
    public override void Initialize(Unit unit) { }

    public void Initialize(bool is_hero, int hp)
    {
        m_is_hero = is_hero;
        m_health.Initialize(hp);

        Renderer.flipX = !is_hero;
    }
    #endregion Helper Methods
}
