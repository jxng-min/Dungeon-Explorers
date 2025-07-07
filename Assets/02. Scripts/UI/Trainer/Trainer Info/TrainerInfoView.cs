using InventoryService;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TrainerInfoView : MonoBehaviour, ITrainerInfoView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [Header("훈련 슬롯")]
    [SerializeField] private TrainerSlotView m_slot;
    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_unit_name;

    [Header("유닛 체력")]
    [SerializeField] private TMP_Text m_unit_hp;

    [Header("유닛 공격력")]
    [SerializeField] private TMP_Text m_unit_atk;

    [Header("강화 현황")]
    [SerializeField] private TMP_Text m_upgrade_label;

    [Header("강화 비용")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("훈련 버튼")]
    [SerializeField] private Button m_training_button;

    [Header("훈련 버튼 라벨")]
    [SerializeField] private TMP_Text m_button_label;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private TrainerInfoPresenter m_presenter;
    private IInventoryService m_inventory_system;
    private IUnitRepository m_unit_repo;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_inventory_system = ServiceLocator.Get<IInventoryService>();
        m_unit_repo = ServiceLocator.Get<IUnitRepository>();

        m_presenter = new TrainerInfoPresenter(this, m_unit_db, m_inventory_system);

        m_training_button.onClick.AddListener(m_presenter.OnClickedUpgrade);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    #region Helper Methods
    public void Initialize(InventoryService.Unit unit)
    {
        m_presenter.Initialize(m_unit_repo, unit);
        m_slot.Initialize(m_unit_db, this, unit);
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
        Updates();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(string name, float hp, int atk, int upgrade, int max_upgrade, int cost, int money)
    {
        m_unit_name.text = $"<color=yellow>{name}</color>";

        m_unit_hp.text = $"체력: {NumberFormatter.FormatNumber(hp)}";
        m_unit_atk.text = $"공격력: {NumberFormatter.FormatNumber(atk)}";

        if (upgrade == max_upgrade)
        {
            m_upgrade_label.text = $"훈련: <color=green>모든 훈련 완료</color>";
        }
        else
        {
            m_upgrade_label.text = $"훈련: {upgrade}/{max_upgrade}";
        }

        if (money < cost)
        {
            m_cost_label.text = $"훈련에 필요한 비용:\t\t<color=red>{NumberFormatter.FormatNumber(cost)}</color>";
            m_training_button.interactable = false;
            m_button_label.text = "<color=red>훈련</color>";
        }
        else
        {
            m_cost_label.text = $"훈련에 필요한 비용:\t\t{NumberFormatter.FormatNumber(cost)}";
            m_training_button.interactable = true;
            m_button_label.text = "훈련";
        }
    }

    public void ResetUI()
    {
        m_unit_hp.text = "";
        m_unit_atk.text = "";
        m_upgrade_label.text = "";
        m_cost_label.text = "";
        m_training_button.interactable = false;
    }
    #endregion Helper Methods
}
