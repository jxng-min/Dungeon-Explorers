using InventoryService;

public class TrainerPresenter
{
    private readonly ITrainerView m_view;
    private readonly ITrainerDataBase m_trainer_db;
    private readonly IInventoryService m_inventory_service;

    private readonly CompactTrainerPresenter m_compact_trainer_presenter;

    private bool m_is_open;

    public TrainerPresenter(ITrainerView view,
                            ITrainerDataBase trainer_db,
                            IInventoryService inventory_service,
                            CompactTrainerPresenter compact_trainer_presenter)
    {
        m_view = view;
        m_trainer_db = trainer_db;
        m_inventory_service = inventory_service;
        m_compact_trainer_presenter = compact_trainer_presenter;  

        m_view.Inject(this);
    }

    private void Initialize()
    {
        foreach(var unit_data in m_inventory_service.Units)
        {
            var trainer_slot_view = m_view.InstantiateSlot();

            var trainer_slot_presenter = new TrainerSlotPresenter(trainer_slot_view,
                                                                  m_trainer_db.GetTrainerData(unit_data.Code),
                                                                  m_compact_trainer_presenter);
        }
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
    }

    public void CloseUI()
    {
        m_is_open = false;
        m_view.CloseUI();
        m_compact_trainer_presenter.CloseUI();
    }
}
