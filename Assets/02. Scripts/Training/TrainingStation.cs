using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrainingStation : MonoBehaviour
{
    [Header("탐험가의 슬롯")]
    [SerializeField] private TrainingCenterSlot m_explorer_slot;

    [Header("탐험가의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("탐험가의 체력 라벨")]
    [SerializeField] private TMP_Text m_hp_label;

    [Header("탐험가의 공격력 라벨")]
    [SerializeField] private TMP_Text m_atk_label;

    [Header("탐험가의 훈련 라벨")]
    [SerializeField] private TMP_Text m_upgrade_label;

    [Header("탐험가의 훈련 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;
    private int m_cost;

    private Animator m_tranining_station_animator;

    private void Awake()
    {
        m_tranining_station_animator = GetComponent<Animator>();
    }

    public void Open(int explorer_id)
    {
        m_tranining_station_animator.SetBool("Open", true);
        Initialize(explorer_id);
    }

    public void BUTTON_Close()
    {
        m_tranining_station_animator.SetBool("Open", false);
    }

    private void Initialize(int id)
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(id);
        if (explorer == null)
        {
            return;
        }

        m_explorer_slot.Initialize(id);
        m_name_label.text = $"<color=green>{explorer.Name}</color>";
        m_hp_label.text = $"체력: {explorer.HP}";
        m_atk_label.text = $"공격력: {explorer.ATK}";
        m_upgrade_label.text = $"강화: {Inventory.Instance.GetItem(id).Upgrade} / {explorer.MaxUpgrade}";
        m_cost_label.text = $"훈련에 필요한 비용:\t\t{explorer.UpgradeCost + ((Inventory.Instance.GetItem(id).Upgrade - 1) * 0.2 * explorer.UpgradeCost)}";
    }
}