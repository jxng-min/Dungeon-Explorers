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

    public void Initialize(IShopView shop_view, IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit)
    {
        m_model.Initialize(shop_view, unit_repo, inventory, unit);
        
        UpdateView();
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.HasUnit, m_model.Money, m_model.Cost);
    }

    public void OnClickedPurchase()
    {
        m_model.Money -= m_model.Cost;
        m_model.AddUnit();

        m_view.Purchase();
        m_model.ShopView.UpdateUI();
        
    }
}
