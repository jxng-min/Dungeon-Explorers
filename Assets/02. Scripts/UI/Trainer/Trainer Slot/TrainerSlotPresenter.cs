using Units;

public class TrainerSlotPresenter
{
    #region Variables
    private readonly ITrainerSlotView m_view;
    private readonly TrainerSlotModel m_model;
    #endregion Variables

    public TrainerSlotPresenter(ITrainerSlotView view)
    {
        m_view = view;
        m_model = new TrainerSlotModel();
    }

    #region Helper Methods
    public void Initialize(UnitDataBase unit_db, ITrainerInfoView trainer_info_view, InventoryService.Unit unit)
    {
        m_model.Initialize(unit_db, trainer_info_view, unit);
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.Image, m_model.Cost);
    }

    public void OnClickedTrainerSlot()
    {
        m_model.InfoView.Initialize(m_model.Unit);
        m_model.InfoView.OpenUI();
    }
    #endregion Helper Methods
}
