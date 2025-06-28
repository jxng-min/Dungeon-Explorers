using InventoryService;
using Units;

public class TrainerInfoPresenter
{
    #region Variables
    private readonly ITrainerInfoView m_view;
    private readonly TrainerInfoModel m_model;
    #endregion Variables

    public TrainerInfoPresenter(ITrainerInfoView view, UnitDataBase unit_db, IInventoryService inventory_system)
    {
        m_view = view;
        m_model = new TrainerInfoModel(unit_db, inventory_system);
    }

    #region Helper Methods
    public void Initialize(IUnitRepository m_unit_repo, InventoryService.Unit unit)
    {
        m_model.Initialize(m_unit_repo, unit);
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }

    public void OnClickedUpgrade()
    {
        m_model.Upgrade++;
        m_model.Money -= m_model.Cost;

        UpdateView();
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.Name, m_model.HP, m_model.ATK, m_model.Upgrade, m_model.MaxUpgrade, m_model.Cost, m_model.Money);
    }
    #endregion Helper Methods
}
