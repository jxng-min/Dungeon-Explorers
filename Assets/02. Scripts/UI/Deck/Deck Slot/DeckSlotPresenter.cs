using System;
using DeckService;
using Units;
using UnitService;

public class DeckSlotPresenter : IDisposable
{
    private readonly IDeckSlotView m_view;
    private readonly IUnitDataBase m_unit_db; 
    private readonly IDeckService m_deck_service;
    private readonly SelectorPresenter m_selector_presenter;
    private readonly UnitCode m_unit_code;

    private bool m_is_selected;

    public DeckSlotPresenter(IDeckSlotView view,
                             IUnitDataBase unit_db,
                             IDeckService deck_service,
                             SelectorPresenter selector_presenter,
                             UnitCode unit_code)
    {
        m_view = view;

        m_unit_db = unit_db;
        m_deck_service = deck_service;
        m_selector_presenter = selector_presenter;

        m_unit_code = unit_code;

        m_deck_service.OnUpdatedDeck += UpdateSelectedSlot; 

        m_view.Inject(this);
        Initialize();
    }

    private void Initialize()
    {
        UpdateUI();
    }

    public void OnClickSlot(System.Numerics.Vector2 mouse_position)
    {
        m_selector_presenter.OpenUI(m_unit_code);
        m_selector_presenter.SetPosition(mouse_position);
    }

    public void UpdateUI()
    {
        var unit = m_unit_db.GetUnit(m_unit_code);
        m_view.UpdateUI(unit.Image, (unit as Hero).Cost);
    }

    public void UpdateSelectedSlot(int index, 
                                   UnitCode legacy_unit_code, 
                                   UnitCode new_unit_code)
    {
        if(m_unit_code == new_unit_code)
        {
            m_is_selected = true;
        }
        else if(m_unit_code == legacy_unit_code)
        {
            m_is_selected = false;
        }

        m_view.UpdateState(m_is_selected);
    }

    public void Dispose()
    {
        m_deck_service.OnUpdatedDeck -= UpdateSelectedSlot;
    }
}
