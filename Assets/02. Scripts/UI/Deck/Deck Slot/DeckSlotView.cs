using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckSlotView : MonoBehaviour, IDeckSlotView, IPointerClickHandler
{
    [Header("UI 관련 컴포넌트")]
    [Header("슬롯 버튼")]
    [SerializeField] private Button m_slot_button;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 상태")]
    [SerializeField] private GameObject m_state_text;

    [Header("유닛 코스트")]
    [SerializeField] private TMP_Text m_cost_label; 

    private DeckSlotPresenter m_presenter;

    private void OnDisable()
    {
        if(m_presenter == null)
        {
            return;
        }

        m_presenter?.Dispose();
    }

    public void Inject(DeckSlotPresenter presenter)
    {
        m_presenter = presenter;
    }

    public void UpdateUI(Sprite unit_image, int unit_cost)
    {
        m_unit_image.sprite = unit_image;
        m_cost_label.text = NumberFormatter.FormatNumber(unit_cost);
    }

    public void UpdateState(bool is_selected)
    {
        m_state_text.SetActive(is_selected);
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var mouse_position = new System.Numerics.Vector2(eventData.position.x, 
                                                         eventData.position.y);
        m_presenter.OnClickSlot(mouse_position);
    }
}
