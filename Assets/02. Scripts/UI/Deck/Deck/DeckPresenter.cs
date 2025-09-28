using DeckService;
using InventoryService;
using UnitService;

public class DeckPresenter
{
    private readonly IDeckView m_view;

    private readonly IUnitDataBase m_unit_db;

    private readonly IInventoryService m_inventory_service;
    private readonly IDeckService m_deck_service;
    private readonly SelectorPresenter m_selector_presenter;

    private bool m_is_open;

    public DeckPresenter(IDeckView view,
                         IUnitDataBase unit_db,
                         IInventoryService inventory_service,
                         IDeckService deck_service,
                         SelectorPresenter selector_presenter)
    {
        m_view = view;

        m_unit_db = unit_db;
        
        m_inventory_service = inventory_service;
        m_deck_service = deck_service;

        m_selector_presenter = selector_presenter;

        m_view.Inject(this);
    }

    public void Initialize()
    {
        foreach(var unit_data in m_inventory_service.Units)
        {
            var deck_slot_view = m_view.InstantiateSlot();

            var deck_slot_presenter = new DeckSlotPresenter(deck_slot_view,
                                                            m_unit_db,
                                                            m_deck_service,
                                                            m_selector_presenter,
                                                            unit_data.Code);
        }
        
        m_deck_service.Initialize();
    }

    public void OpenUI()
    {
        if(m_is_open)
        {
            return;
        }

        m_is_open = true;

        m_view.OpenUI();
        Initialize();

        m_view.PlaySFX("Button Click");
    }

    public void CloseUI()
    {
        m_is_open = false;
        m_view.CloseUI();
        m_selector_presenter.CloseUI();
    }
}
