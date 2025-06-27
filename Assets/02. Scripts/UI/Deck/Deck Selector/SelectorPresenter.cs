using Units;
using UnityEngine;

public class SelectorPresenter
{
    #region Variables
    private readonly ISelectorView m_view;
    private readonly SelectorModel m_model;
    #endregion Variables

    public SelectorPresenter(ISelectorView view, IDeckView deck_view)
    {
        m_view = view;
        m_model = new SelectorModel(deck_view);
    }

    public void OpenSelector(IDeckSlotView deck_slot, Unit unit, Vector2 touch_position, bool is_candidate)
    {
        m_model.Unit = unit;

        if (m_model.Mode == SelectorWorkingMode.EQUIPPING)
        {
            m_view.SetHightlightSlots(false);

            CloseSelector();

            return;
        }

        m_model.DeckSlot = deck_slot;

        if (m_model.Unit != null)
        {
            m_view.OpenUI(touch_position, is_candidate);
        }
    }

    public void CloseSelector()
    {
        m_model.Mode = SelectorWorkingMode.NONE;
        m_model.Unit = null;

        m_view.CloseUI();
    }

    public void OnClickedEquipment()
    {
        m_model.Mode = SelectorWorkingMode.EQUIPPING;

        m_view.SetHightlightSlots(true);
    }

    public void OnClickedDissolved()
    {
        m_model.Mode = SelectorWorkingMode.DISSOLVING;

        m_model.DeckSlot.Clear();
        m_model.DeckView.UpdateUI();

        m_view.SetHightlightSlots(false);
        m_view.CloseUI();
    }
}
