using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventoryService;

public class ShopSlotView : MonoBehaviour, IShopSlotView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_unit_name;

    [Header("유닛 가격")]
    [SerializeField] private TMP_Text m_unit_cost;

    [Header("구매 버튼")]
    [SerializeField] private Button m_purchase_button;

    [Header("비활성화 이미지")]
    [SerializeField] private GameObject m_disabled_object;

    private ShopSlotPresenter m_presenter;

    #endregion Variables

    private void Awake()
    {
        m_presenter = new ShopSlotPresenter(this);

        m_purchase_button.onClick.AddListener(m_presenter.OnClickedPurchase);
    }

    public void Initialize(IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit)
    {
        m_presenter.Initialize(unit_repo, inventory, unit);

        m_unit_image.sprite = unit.Image;
        m_unit_name.text = unit_repo.GetName(unit.Code);
    }

    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(bool has_unit, int money, int cost)
    {
        m_unit_cost.text = NumberFormatter.FormatNumber(cost);
        m_disabled_object.SetActive(false);

        if (has_unit)
        {
            m_disabled_object.SetActive(true);
            m_purchase_button.interactable = false;
            return;
        }

        if (money < cost)
        {
            m_unit_cost.text = $"<color=red>{NumberFormatter.FormatNumber(cost)}</color>";
            m_purchase_button.interactable = false;
            return;
        }
    }

    public void Purchase()
    {
        m_disabled_object.SetActive(true);
    }
}
