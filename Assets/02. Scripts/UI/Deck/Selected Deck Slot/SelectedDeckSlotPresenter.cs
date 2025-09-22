using System;
using DeckService;
using Units;
using UnitService;

public class SelectedDeckSlotPresenter : IDisposable
{
    private readonly ISelectedDeckSlotView m_view;
    private readonly IUnitDataBase m_unit_db;
    private readonly IDeckService m_deck_service;

    private SelectorPresenter m_selector_presenter;
    
    private readonly int m_index;
    private UnitCode m_unit_code;

    public SelectedDeckSlotPresenter(ISelectedDeckSlotView view,
                                     IUnitDataBase unit_db,
                                     IDeckService deck_service,
                                     int index)
    {
        m_view = view;
        m_unit_db = unit_db;
        m_deck_service = deck_service;
        m_index = index;

        m_deck_service.OnUpdatedDeck += UpdateSelectedSlot;

        m_view.Inject(this);
    }

    public void Inject(SelectorPresenter selector_presenter)
    {
        m_selector_presenter = selector_presenter;
    }

    public void UpdateSelectedSlot(int index, 
                                   UnitCode legacy_unit_code, 
                                   UnitCode new_unit_code)
    {
        if(m_index != index)
        {
            return;
        }

        m_unit_code = new_unit_code;

        if(m_unit_code != UnitCode.EMPTY)
        {
            var unit = m_unit_db.GetUnit(m_unit_code);
            m_view.UpdateUI(unit.Image, (unit as Hero).Cost);
        }
        else
        {
            m_view.UpdateUI(null, 0);
        }
    }

    public void Hightlight(bool active)
    {
        m_view.SetHighlight(active);
    }

    public void OnClickSlot(System.Numerics.Vector2 mouse_position)
    {
        if(m_selector_presenter.UnitCode == UnitCode.EMPTY)
        {
            m_selector_presenter.OpenUI(m_unit_code);
            m_selector_presenter.SetPosition(mouse_position);
        }
        else
        {
            m_deck_service.SetDeck(m_index, m_selector_presenter.UnitCode);
            m_selector_presenter.CloseUI();
        }
    }

    public void Dispose()
    {
        m_deck_service.OnUpdatedDeck -= UpdateSelectedSlot;
    }
}
