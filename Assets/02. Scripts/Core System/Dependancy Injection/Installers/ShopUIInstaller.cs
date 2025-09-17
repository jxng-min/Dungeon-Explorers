using InventoryService;
using UnitService;
using UnityEngine;

public class ShopUIInstaller : MonoBehaviour, IInstaller
{
    [Header("상점 뷰")]
    [SerializeField] private ShopView m_shop_view;

    [Header("상점 데이터베이스")]
    [SerializeField] private ShopDataBase m_shop_db;

    public void Install()
    {
        InstallShop();
    }

    private void InstallShop()
    {
        DIContainer.Register<IShopView>(m_shop_view);

        var shop_presenter = new ShopPresenter(m_shop_view,
                                               m_shop_db,
                                               ServiceLocator.Get<IInventoryService>(),
                                               ServiceLocator.Get<IUnitService>());
        DIContainer.Register<ShopPresenter>(shop_presenter);
    }
}
