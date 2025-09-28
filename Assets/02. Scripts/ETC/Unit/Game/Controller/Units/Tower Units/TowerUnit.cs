using UnityEngine;

public class TowerUnit : BaseUnit
{
    #region Variables
    private bool m_is_hero;

    [Header("타워 UI 뷰")]
    [SerializeField] private TowerView m_tower_view;
    #endregion Variables

    public bool IsHero { get => m_is_hero; }
    public ITowerView TowerView { get => m_tower_view; }

    protected override void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        Health = GetComponent<IHealth>();
    }

    public override void Initialize(Unit unit) { }

    public void Initialize(bool is_hero, int hp)
    {
        m_is_hero = is_hero;
        Renderer.flipX = !is_hero;

        Health.Initialize(hp);
    }
}
