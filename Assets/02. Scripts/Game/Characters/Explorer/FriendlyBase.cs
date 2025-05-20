using TMPro;
using UnityEngine;

public class FriendlyBase : Character
{
    [Header("현재 체력을 나타낼 라벨")]
    [SerializeField] private TMP_Text m_current_hp_label;

    [Header("최대 체력을 나타낼 라벨")]
    [SerializeField] private TMP_Text m_max_hp_label;

    private int m_default_hp = 500;
    private const int GROWTH = 25;

    private int m_max_hp;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        m_max_hp = m_default_hp + GROWTH * DataManager.Instance.Data.Reinforcement.TowerHP;
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
        GameEventBus.Publish(GameEventType.GAMEOVER);   
    }

    protected override void Attack()
    {
        // 비워둡니다.
    }
}
