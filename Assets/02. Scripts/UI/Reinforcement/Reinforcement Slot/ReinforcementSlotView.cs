using InventoryService;
using ReinforcementService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcementSlotView : MonoBehaviour, IReinforcementSlotView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("능력치 이름 라벨")]
    [SerializeField] private TMP_Text m_element_name;

    [Header("능력치 이미지")]
    [SerializeField] private Image m_element_image;

    [Header("능력치 레벨 라벨")]
    [SerializeField] private TMP_Text m_element_level;

    [Header("강화 비용 라벨")]
    [SerializeField] private TMP_Text m_upgrade_cost;

    [Header("강화 버튼 라벨")]
    [SerializeField] private TMP_Text m_upgrade_button_label;

    [Header("강화 버튼")]
    [SerializeField] private Button m_upgrade_button;

    private ReinforcementSlotPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_presenter = new ReinforcementSlotPresenter(this);
    }

    #region Helper Methods
    public void Initialize(ReinforceDataBase db, IReinforcementService reinforce_service, IInventoryService inventory_service, ReinforcementType type)
    {
        m_presenter.Initialize(reinforce_service, inventory_service, type);

        var item = db.GetItem(type);
        m_element_name.text = item.Name;
        m_element_image.sprite = item.Image;

        m_upgrade_button.onClick.AddListener(m_presenter.OnClickedUpgrade);
    }

    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(int level, int money, int cost)
    {
        m_element_level.text = $"LV.{NumberFormatter.FormatNumber(level + 1)}";

        m_upgrade_cost.text = $"강화 비용: {NumberFormatter.FormatNumber(cost)}";

        m_upgrade_button_label.text = "강화";
        m_upgrade_button.interactable = true;

        if (money < cost)
        {
            var cost_string = NumberFormatter.FormatNumber(cost);
            m_upgrade_cost.text = $"강화 비용: <color=red>{cost_string}</color>";

            m_upgrade_button_label.text = "<color=red>강화</color>";
            m_upgrade_button.interactable = false;
        }
    }
    #endregion Helper Methods
}
