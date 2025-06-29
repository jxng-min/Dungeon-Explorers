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

    public void Initialize(UnitDataBase unit_db, IDeckService deck_system, IDeckView deck_view, ISelectorView selector_view, UnitCode code)
    {
        m_model.Initialize(unit_db, deck_system, deck_view, selector_view, code);
    }

    public void Swap(UnitCode code)
    {
        m_model.Code = code;
        Debug.Log("설정함");

        for (int i = 0; i < m_model.Deck.Count; i++)
        {
            if (m_model.Code == m_model.DeckView.GetSlotView(i).GetCode())
            {
                m_model.DeckSystem.SetDeck(i, m_model.Code);
                Debug.Log($"들어옴: {i}");
            }
        }
    }

    public void UpdateView()
    {
        if (m_model.Unit == null || m_model.Unit.Code == UnitCode.EMPTY)
        {
            m_view.ClearUI();
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
                m_model.Code = UnitCode.EMPTY;
                m_model.DeckSystem.SetDeck(i, UnitCode.EMPTY);
                break;
            }
        }

        m_view.ClearUI();
    }

    public void OnClickedSlot(Vector2 touch_position)
    {
        for (int i = 0; i < m_model.Deck.Count; i++)
        {
            if (m_model.Unit != null && m_model.Deck[i] == m_model.Unit.Code)
            {
                m_model.SelectorView.Initialize(m_model.DeckView.GetSlotView(i), m_model.Unit, touch_position, false);
                return;
            }
        }

        m_model.SelectorView.Initialize(m_view, m_model.Unit, touch_position, true);
    }

    public UnitCode GetCode()
    {
        return m_model.Code;
    }
}
