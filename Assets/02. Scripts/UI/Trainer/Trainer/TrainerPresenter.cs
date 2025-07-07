using InventoryService;

public class TrainerPresenter
{
    #region Variables
    private readonly ITrainerView m_view;
    private readonly TrainerModel m_model;
    #endregion Variables

    public TrainerPresenter(ITrainerView view, IInventoryService inventory_system)
    {
        m_view = view;
        m_model = new TrainerModel(inventory_system);
    }

    public void OnClickedOpenUI()
    {
        m_view.InstantiateSlots(m_model.InventorySystem.Units);
        m_view.OpenUI();
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }
}
