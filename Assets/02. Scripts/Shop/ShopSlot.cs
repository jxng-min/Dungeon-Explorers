using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_slot_image;

    [Header("탐험가의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("탐험가의 가격 라벨")]
    [SerializeField] private TMP_Text m_price_label;

    [Header("슬롯의 구매 버튼")]
    [SerializeField] private Button m_purchase_button;

    [Header("슬롯의 구매 방지 마스크")]
    [SerializeField] private GameObject m_mask;

    [Space(30f)][Header("탐험가의 정보")]
    [SerializeField] private Explorer m_explorer;

    public void Initialize()
    {
        m_slot_image.sprite = m_explorer.Image;
        m_name_label.text = m_explorer.Name;
        m_price_label.text = m_explorer.Price.ToString();
    }

    public void UpdateSlot()
    {
        m_mask.SetActive(false);
        m_price_label.color = Color.white;

        if(Inventory.Instance.CheckHasItem(m_explorer.ID))
        {
            m_purchase_button.interactable = false;
            m_mask.SetActive(true);
            return;
        }

        if(DataManager.Instance.Data.Money < m_explorer.Price)
        {
            m_purchase_button.interactable = false;
            m_price_label.color = Color.red;
            return;
        }

        m_purchase_button.interactable = true;
    }

    public void BUTTON_Purchase()
    {
        if(DataManager.Instance.Data.Money >= m_explorer.Price)
        {
            Inventory.Instance.TryAdd(m_explorer.ID);
            DataManager.Instance.Data.Money -= m_explorer.Price;
        }

        var shop = FindFirstObjectByType<Shop>();
        shop.UpdateSlots();
    }
}
