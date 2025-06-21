using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TrainingStation : MonoBehaviour
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 슬롯")]
    [SerializeField] private TrainingCenterSlot m_unit_slot;

    [Header("유닛 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("유닛 체력 라벨")]
    [SerializeField] private TMP_Text m_hp_label;

    [Header("유닛 공격력 라벨")]
    [SerializeField] private TMP_Text m_atk_label;

    [Header("유닛 훈련 라벨")]
    [SerializeField] private TMP_Text m_upgrade_label;

    [Header("유닛 훈련 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("훈련 버튼")]
    [SerializeField] private Button m_upgrade_button;
    
    private UnitCode m_unit_code;
    private int m_upgrade_cost;

    private Animator m_animator;
    #endregion Variables 

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void OpenUI(UnitCode code)
    {
        m_animator.SetBool("Open", true);
        Initialize(code);
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void BUTTON_Upgrade()
    {
        if(DataManager.Instance.Data.Money < m_upgrade_cost)
        {
            return;
        }

        DataManager.Instance.Data.Money -= m_upgrade_cost;
        Inventory.Instance.GetItem(m_unit_code).Upgrade++;

        UpdateStation();
    }

    private void Initialize(UnitCode code)
    {
        var unit = m_unit_db.GetUnit(code);
        if (unit == null)
        {
            return;
        }

        m_unit_code = code;
        m_unit_slot.Initialize(m_unit_db, m_unit_code);
        m_name_label.text = $"<color=green>{ServiceLocator.Instance.UnitRepoService.GetName(m_unit_code)}</color>";
        UpdateStation();
    }

    private void UpdateStation()
    {
        var explorer = m_unit_db.GetUnit(m_unit_code) as Hero;
        if (explorer == null)
        {
            return;
        }

        m_hp_label.text = $"체력: {explorer.HP + explorer.GrowthHP * (Inventory.Instance.GetItem(m_unit_code).Upgrade - 1)}";
        m_atk_label.text = $"공격력: {explorer.ATK + explorer.GrowthATK * (Inventory.Instance.GetItem(m_unit_code).Upgrade - 1)}";
        m_upgrade_label.text = $"강화: {Inventory.Instance.GetItem(m_unit_code).Upgrade} / {explorer.MaxUpgrade}";

        m_upgrade_cost = explorer.UpgradeCost + (int)((Inventory.Instance.GetItem(m_unit_code).Upgrade - 1) * 0.2 * explorer.UpgradeCost);

        if (Inventory.Instance.GetItem(m_unit_code).Upgrade < explorer.MaxUpgrade)
        {
            if (DataManager.Instance.Data.Money < m_upgrade_cost)
            {
                m_upgrade_button.interactable = false;
                m_cost_label.text = $"훈련에 필요한 비용:\t\t<color=red>{NumberFormatter.FormatNumber(m_upgrade_cost)}</color>";
            }
            else
            {
                m_upgrade_button.interactable = true;
                m_cost_label.text = $"훈련에 필요한 비용:\t\t{NumberFormatter.FormatNumber(m_upgrade_cost)}";
            }
        }
        else
        {
            m_upgrade_button.interactable = false;
            m_cost_label.text = $"<color=yellow>모든 강화 완료</color>";
        }
    }
}