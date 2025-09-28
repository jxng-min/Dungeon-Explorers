using InventoryService;
using UnitService;

public class ShopPresenter
{
    private readonly IShopView m_view;

    private readonly IShopDataBase m_shop_db;

    private readonly IInventoryService m_inventory_service;
    private readonly IUnitService m_unit_service;

    public ShopPresenter(IShopView view,
                         IShopDataBase shop_db,
                         IInventoryService inventory_service,
                         IUnitService unit_service)
    {
        m_view = view;

        m_shop_db = shop_db;

        m_inventory_service = inventory_service;
        m_unit_service = unit_service;

        m_view.Inject(this);
    }

    public void Initialize()
    {
        foreach(var shop_data in m_shop_db.List)
        {
            var shop_slot_view = m_view.InstantiateSlot();

            var shop_slot_presenter = new ShopSlotPresenter(shop_slot_view,
                                                            m_inventory_service,
                                                            m_unit_service,
                                                            shop_data);
        }

        m_inventory_service.Initialize();
    }

    public void OpenUI()
    {
        m_view.OpenUI();
        m_view.PlaySFX("Button Click");
    }

    public void CloseUI()
    {
        m_view.CloseUI();
    }
}
