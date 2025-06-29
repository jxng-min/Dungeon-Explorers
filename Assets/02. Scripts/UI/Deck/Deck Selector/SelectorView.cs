using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class SelectorView : MonoBehaviour, ISelectorView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("덱 편성기")]
    [SerializeField] private DeckView m_deck_view;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("UI 캔버스")]
    [SerializeField] private Canvas m_canvas;

    [Header("버튼의 부모 트랜스폼")]
    [SerializeField] private GameObject m_buttons_frame;

    [Header("[장착] 버튼")]
    [SerializeField] private Button m_equipment_button;

    [Header("[장착 해제] 버튼")]
    [SerializeField] private Button m_dissolved_button;

    [Header("[선택 취소] 버튼")]
    [SerializeField] private List<Button> m_back_buttons;

    private SelectorPresenter m_presenter;
    #endregion Variables

    #region Properties
    public SelectorPresenter Presenter { get => m_presenter; }
    #endregion Properties

    private void Awake()
    {
        m_presenter = new SelectorPresenter(this, m_deck_view);

        m_equipment_button.onClick.AddListener(m_presenter.OnClickedEquipment);
        m_dissolved_button.onClick.AddListener(m_presenter.OnClickedDissolved);

        for (int i = 0; i < m_back_buttons.Count; i++)
        {
            m_back_buttons[i].onClick.AddListener(m_presenter.CloseSelector);
        }
    }

    public void Initialize(IDeckSlotView deck_slot, Unit unit, Vector2 touch_position, bool is_candidate)
    {
        m_presenter.OpenSelector(deck_slot, unit, touch_position, is_candidate);
    }

    public void OpenUI(Vector2 touch_position, bool is_candidate)
    {
        CalculateTouchPosition(touch_position);
        m_buttons_frame.SetActive(true);

        if (is_candidate)
        {
            m_equipment_button.gameObject.SetActive(true);
        }
        else
        {
            m_dissolved_button.gameObject.SetActive(true);
        }

        foreach (var back_button in m_back_buttons)
        {
            back_button.gameObject.SetActive(true);
        }
    }

    public void CloseUI()
    {
        m_equipment_button.gameObject.SetActive(false);
        m_dissolved_button.gameObject.SetActive(false);
        m_buttons_frame.SetActive(false);

        foreach (var back_button in m_back_buttons)
        {
            back_button.gameObject.SetActive(false);
        }

        SetHightlightSlots(false);
    }

    private void CalculateTouchPosition(Vector2 touch_position)
    {
        var canvas_rect_transform = m_canvas.GetComponent<RectTransform>();
        var rect_transform = m_buttons_frame.transform as RectTransform;

        Camera ui_camera = m_canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : m_canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas_rect_transform,
            touch_position,
            ui_camera,
            out Vector2 local_position
        );

        if (touch_position.x < Screen.width * 0.15f)
        {
            local_position.x += rect_transform.sizeDelta.x;
        }

        if (touch_position.y < Screen.height * 0.85f)
        {
            local_position.y -= rect_transform.sizeDelta.y;
        }

        rect_transform.anchoredPosition = local_position;
    }

    public void SetHightlightSlots(bool flag)
    {
        m_deck_view.SetHighlightSlots(flag);
    }
}
