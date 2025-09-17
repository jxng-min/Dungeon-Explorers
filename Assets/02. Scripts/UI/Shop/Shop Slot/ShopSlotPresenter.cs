using System;
using InventoryService;
using UnitService;

public class ShopSlotPresenter : IDisposable
{
    private readonly IShopSlotView m_view;
    private readonly IInventoryService m_inventory_service;
    private readonly IUnitService m_unit_service;
    private readonly ShopData m_shop_data;

    public ShopSlotPresenter(IShopSlotView view,
                             IInventoryService inventory_service,
                             IUnitService unit_service,
                             ShopData shop_data)
    {
        m_view = view;
        m_inventory_service = inventory_service;
        m_unit_service = unit_service;
        m_shop_data = shop_data;

        m_inventory_service.OnUpdatedMoney += UpdateMoney;
        m_inventory_service.OnUpdatedUnit += UpdateUnit;

        m_view.Inject(this);
        Initialize();
    }

    private void Initialize()
    {
        m_view.UpdateUI(m_unit_service.GetName(m_shop_data.Hero.Code), m_shop_data.Hero.Image);

        m_view.UpdateAquire(m_inventory_service.HasUnit(m_shop_data.Hero.Code));
    }

    public void PurchaseUnit()
    {
        m_inventory_service.UpdateMoney(-m_shop_data.Cost);
        m_inventory_service.AddUnit(m_shop_data.Hero.Code);
    }

    private void UpdateMoney(int money)
    {
        m_view.UpdatePurchase(m_shop_data.Cost, m_shop_data.Cost <= money);
    }

    private void UpdateUnit(UnitData unit)
    {
        if(unit.Code != m_shop_data.Hero.Code)
        {
            return;
        }
        
        m_view.UpdateAquire(m_inventory_service.HasUnit(unit.Code));
    }

    public void Dispose()
    {
        m_inventory_service.OnUpdatedMoney -= UpdateMoney;
        m_inventory_service.OnUpdatedUnit -= UpdateUnit;        
    }
}
