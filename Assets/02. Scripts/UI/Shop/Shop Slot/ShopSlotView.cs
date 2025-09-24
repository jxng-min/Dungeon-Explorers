using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotView : MonoBehaviour, IShopSlotView
{
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 가격")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("구매 버튼")]
    [SerializeField] private Button m_purchase_button;

    [Header("비활성화 패널")]
    [SerializeField] private GameObject m_disabled_panel;

    private ShopSlotPresenter m_presenter;

    private void OnDestroy()
    {
        m_presenter?.Dispose();
    }

    public void Inject(ShopSlotPresenter presenter)
    {
        m_presenter = presenter;

        m_purchase_button.onClick.AddListener(m_presenter.PurchaseUnit);
    }

    public void UpdateUI(string unit_name, 
                         Sprite unit_image)
    {
        m_name_label.text = unit_name;
        m_unit_image.sprite = unit_image;
    }

    public void UpdatePurchase(int cost, bool can_purchase)
    {
        m_purchase_button.interactable = can_purchase;

        m_cost_label.text = can_purchase ?
                            $"<color=white>{NumberFormatter.FormatNumber(cost)}</color>" :
                            $"<color=red>{NumberFormatter.FormatNumber(cost)}</color>";
    }

    public void UpdateAquire(bool has_unit)
    {
        if(has_unit)
        {
            m_purchase_button.gameObject.SetActive(false);
            m_disabled_panel.SetActive(true);
        }
        else
        {
            m_purchase_button.gameObject.SetActive(true);
            m_disabled_panel.SetActive(false);
        }
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }
}
