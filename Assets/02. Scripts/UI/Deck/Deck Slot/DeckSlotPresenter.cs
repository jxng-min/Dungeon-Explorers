using DeckService;
using Units;
using UnityEngine;

public class DeckSlotPresenter
{
    #region Variables
    private readonly IDeckSlotView m_view;
    private readonly DeckSlotModel m_model;
    #endregion Variables

    public DeckSlotPresenter(IDeckSlotView view)
    {
        m_view = view;
        m_model = new DeckSlotModel();
    }

    public void Initialize(UnitDataBase unit_db, IDeckService deck_system, ISelectorView selector_view, UnitCode code, bool is_candidate)
    {
        m_model.Initialize(unit_db, deck_system, selector_view, code, is_candidate);
    }

    public void UpdateView()
    {
        if (m_model.Unit == null || m_model.Unit.Code == UnitCode.EMPTY)
        {
            return;
        }

        var deck = m_model.Deck;
        var is_selected = deck.Contains(m_model.Unit.Code);

        m_view.UpdateUI(m_model.Image, m_model.Cost, is_selected);
    }

    public void ClearView()
    {
        for (int i = 0; i < m_model.Deck.Count; i++)
        {
            if (m_model.Unit != null && m_model.Deck[i] == m_model.Unit.Code)
            {
                m_model.DeckSystem.SetDeck(i, UnitCode.EMPTY);
                break;
            }
        }

        m_view.ClearUI();
    }

    public void OnClickedSlot(Vector2 touch_position)
    {
        m_model.SelectorView.Initialize(m_view, m_model.Unit, touch_position, m_model.IsCandidate);
    }
}
