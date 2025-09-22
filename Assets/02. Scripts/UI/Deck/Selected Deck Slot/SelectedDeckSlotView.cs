using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SelectedDeckSlotView : MonoBehaviour, ISelectedDeckSlotView, IPointerClickHandler
{
    [Header("UI 관련 컴포넌트")]
    [Header("슬롯 버튼")]
    [SerializeField] private Button m_slot_button;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("코스트 프레임")]
    [SerializeField] private GameObject m_cost_frame;

    [Header("유닛 코스트")]
    [SerializeField] private TMP_Text m_cost_label;

    private Animator m_animator;

    private SelectedDeckSlotPresenter m_presenter;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        m_presenter.Dispose();
    }

    public void Inject(SelectedDeckSlotPresenter presenter)
    {
        m_presenter = presenter;
    }

    public void UpdateUI(Sprite unit_image, int unit_cost)
    {
        if(unit_image == null)
        {
            SetAlpha(0f);

            m_cost_label.text = string.Empty;
            m_cost_frame.SetActive(false);
        }
        else
        {
            m_unit_image.sprite = unit_image;
            SetAlpha(1f);

            m_cost_frame.SetActive(true);
            m_cost_label.text = NumberFormatter.FormatNumber(unit_cost);
        }
    }

    public void SetHighlight(bool active)
    {
        m_animator.SetBool("Highlight", active);
    }

    private void SetAlpha(float alpha)
    {
        var color = m_unit_image.color;
        color.a = alpha;
        m_unit_image.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var mouse_position = new System.Numerics.Vector2(eventData.position.x, 
                                                         eventData.position.y);

        m_presenter.OnClickSlot(mouse_position);
    }
}
