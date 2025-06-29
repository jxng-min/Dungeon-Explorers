using System;
using DeckService;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DeckSlotView : MonoBehaviour, IDeckSlotView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("비용 프레임 오브젝트")]
    [SerializeField] private GameObject m_cost_frame;

    [Header("비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("선택 공지 오브젝트")]
    [SerializeField] private GameObject m_selected_object;

    private Animator m_animator;
    private DeckSlotPresenter m_presenter;

    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_presenter = new DeckSlotPresenter(this);
    }

    #region Helper Methods
    public void Initialize(UnitDataBase unit_db, IDeckService deck_system, IDeckView deck_view, ISelectorView selector_view, UnitCode code)
    {
        m_presenter.Initialize(unit_db, deck_system, deck_view, selector_view, code);
        ClearUI();
    }

    public void Swap(UnitCode code)
    {
        m_presenter.Swap(code);
    }

    public void Clear()
    {
        m_presenter.ClearView();
    }

    public void ClearUI()
    {
        m_unit_image.sprite = null;
        SetAlpha(0f);

        m_cost_label.text = "";
        m_cost_frame.SetActive(false);

        m_selected_object.SetActive(false);
    }

    private void SetAlpha(float alpha)
    {
        var color = m_unit_image.color;
        color.a = alpha;
        m_unit_image.color = color;
    }

    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(Sprite unit_sprite, int cost, bool is_selected)
    {
        m_unit_image.sprite = unit_sprite;
        SetAlpha(1f);

        m_cost_frame.SetActive(true);
        m_cost_label.text = NumberFormatter.FormatNumber(cost);

        m_selected_object.SetActive(is_selected);
    }

    public void SetHighlight(bool flag)
    {
        m_animator.SetBool("Glow", flag);
    }

    public UnitCode GetCode()
    {
        return m_presenter.GetCode();
    }
    #endregion Helper Methods

    #region Event Methods
    public void OnPointerClick(PointerEventData eventData)
    {
        m_presenter.OnClickedSlot(eventData.position);
    }
    #endregion Event Methods
}
