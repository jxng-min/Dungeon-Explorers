using InventoryService;

public class ShopSlotPresenter
{
    #region Variables
    private readonly IShopSlotView m_view;
    private readonly ShopSlotModel m_model;
    #endregion Variables

    public ShopSlotPresenter(IShopSlotView view)
    {
        m_view = view;
        m_model = new ShopSlotModel();
    }

    public void Initialize(IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit)
    {
        m_model.Initialize(unit_repo, inventory, unit);
        
        UpdateView();
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.HasUnit(), m_model.GetMoney(), m_model.GetCost());
    }

    public void OnClickedPurchase()
    {
        m_model.UpdateMoney(-m_model.GetCost());
        m_model.AddUnit();

        m_view.Purchase();
        m_view.UpdateUI(m_model.HasUnit(), m_model.GetMoney(), m_model.GetCost());
    }
}
