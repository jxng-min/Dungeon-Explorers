using InventoryService;
using ReinforcementService;

public class ReinforcementSlotPresenter
{
    #region Variables
    private readonly IReinforcementSlotView m_view;
    private readonly ReinforcementSlotModel m_model;
    #endregion Variables

    public ReinforcementSlotPresenter(IReinforcementSlotView view)
    {
        m_view = view;
        m_model = new ReinforcementSlotModel();
    }

    #region Helper Methods
    public void Initialize(IReinforcementService reinforce_service, IInventoryService inventory_service, ReinforcementType type)
    {
        m_model.Initialize(reinforce_service, inventory_service, type);
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.GetField(), m_model.GetMoney(), m_model.GetCost());
    }

    public void OnClickedUpgrade()
    {
        m_model.UpdateMoney(-m_model.GetCost());
        m_model.UpgradeField();

        UpdateView();
    }
    #endregion Helper Methods
}
