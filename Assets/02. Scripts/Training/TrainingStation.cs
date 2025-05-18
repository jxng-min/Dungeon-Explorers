using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    private int m_explorer_id;
    private int m_cost;

    [Header("훈련 버튼")]
    [SerializeField] private Button m_upgrade_button;

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

    public void BUTTON_Upgrade()
    {
        if(DataManager.Instance.Data.Money < m_cost)
        {
            return;
        }

        DataManager.Instance.Data.Money -= m_cost;
        Inventory.Instance.GetItem(m_explorer_id).Upgrade++;

        UpdateStation();
    }

    private void Initialize(int id)
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(id);
        if (explorer == null)
        {
            return;
        }

        m_explorer_id = id;
        m_explorer_slot.Initialize(m_explorer_id);
        m_name_label.text = $"<color=green>{explorer.Name}</color>";
        UpdateStation();
    }

    private void UpdateStation()
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(m_explorer_id);
        if (explorer == null)
        {
            return;
        }

        m_hp_label.text = $"체력: {explorer.HP + explorer.GrowthHP * (Inventory.Instance.GetItem(m_explorer_id).Upgrade - 1)}";
        m_atk_label.text = $"공격력: {explorer.ATK + explorer.GrowthATK * (Inventory.Instance.GetItem(m_explorer_id).Upgrade - 1)}";
        m_upgrade_label.text = $"강화: {Inventory.Instance.GetItem(m_explorer_id).Upgrade} / {explorer.MaxUpgrade}";

        m_cost = explorer.UpgradeCost + (int)((Inventory.Instance.GetItem(m_explorer_id).Upgrade - 1) * 0.2 * explorer.UpgradeCost);

        if (Inventory.Instance.GetItem(m_explorer_id).Upgrade < explorer.MaxUpgrade)
        {
            if (DataManager.Instance.Data.Money < m_cost)
            {
                m_upgrade_button.interactable = false;
                m_cost_label.text = $"훈련에 필요한 비용:\t\t<color=red>{m_cost}</color>";
            }
            else
            {
                m_upgrade_button.interactable = true;
                m_cost_label.text = $"훈련에 필요한 비용:\t\t{m_cost}";
            }
        }
        else
        {
            m_upgrade_button.interactable = false;
            m_cost_label.text = $"<color=yellow>모든 강화 완료</color>";
        }
    }
}