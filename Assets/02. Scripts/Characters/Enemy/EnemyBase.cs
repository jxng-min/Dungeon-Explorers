using TMPro;
using UnityEngine;

public class EnemyBase : EnemyCtrl
{
    [Header("몬스터 팩토리")]
    [SerializeField] private EnemyFactory m_factory;

    [Header("현재 체력을 나타낼 라벨")]
    [SerializeField] private TMP_Text m_current_hp_label;

    [Header("최대 체력을 나타낼 라벨")]
    [SerializeField] private TMP_Text m_max_hp_label;

    private int m_max_hp;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        m_max_hp = m_factory.Stage.BaseHP;
        m_current_hp = m_max_hp;

        UpdateUI();
    }

    private void UpdateUI()
    {
        m_current_hp_label.text = NumberFormatter.FormatNumber(m_current_hp);
        m_max_hp_label.text = NumberFormatter.FormatNumber(m_max_hp);
    }

    public override void UpdateHP(int amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_current_hp += amount;
        UpdateUI();

        if (m_current_hp <= 0f)
        {
            m_current_hp = 0f;
            UpdateUI();

            Death();
        }
    }

    public override void Death()
    {
        GameEventBus.Publish(GameEventType.GAMECLEAR);   
    }

    protected override void Attack()
    {
        // 비워둡니다.
    }
}
