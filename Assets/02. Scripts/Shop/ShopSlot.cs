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
        // TODO: 구매 가능한지의 여부에 따라 슬롯의 상태를 업데이트
    }

    public void BUTTON_Purchase()
    {
        // TODO: 구매 처리
    }
}
