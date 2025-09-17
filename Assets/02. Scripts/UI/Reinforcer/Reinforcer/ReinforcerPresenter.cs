using InventoryService;
using ReinforcerService;

public class ReinforcerPresenter
{
    private readonly IReinforcerView m_view;

    private readonly IReinforcerDataBase m_reinforcer_db;
    
    private readonly IInventoryService m_inventory_service;
    private readonly IReinforcerService m_reinforcer_service;

    public ReinforcerPresenter(IReinforcerView view,
                               IInventoryService inventory_service,
                               IReinforcerService reinforcer_service,
                               IReinforcerDataBase reinforcer_db)
    {
        m_view = view;

        m_inventory_service = inventory_service;
        m_reinforcer_service = reinforcer_service;

        m_reinforcer_db = reinforcer_db;

        m_view.Inject(this);
    } 

    public void Initialize()
    {
        foreach(var reinforcement_item in m_reinforcer_db.List)
        {
            var reinforcer_slot_view = m_view.InstantiateSlot();

            var reinforcer_slot_presenter = new ReinforcerSlotPresenter(reinforcer_slot_view,
                                                                        m_inventory_service,
                                                                        m_reinforcer_service,
                                                                        reinforcement_item);
        }
    }

    public void OpenUI()
    {
        m_view.OpenUI();
    }

    public void CloseUI()
    {
        m_view.CloseUI();
    }
}
