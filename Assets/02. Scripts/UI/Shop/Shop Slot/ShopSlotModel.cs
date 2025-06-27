using InventoryService;
using Units;
using UnityEngine;

public class ShopSlotModel
{
    #region Variables
    private Units.Unit m_unit;
    private IInventoryService m_inventory;
    private IUnitRepository m_unit_repo;
    private IShopView m_shop_view;
    #endregion Variables

    #region Properties
    public IShopView ShopView { get => m_shop_view; }
    public int Money
    {
        get => m_inventory.Money;
        set => m_inventory.Money = value;
    }
    public bool HasUnit { get => m_inventory.HasUnit(m_unit.Code); }
    public int Cost { get => (m_unit as Hero).Price; }
    #endregion Properties

    #region Helper Methods
    public void Initialize(IShopView shop_view, IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit)
    {
        m_shop_view = shop_view;
        m_unit = unit;
        m_inventory = inventory;
        m_unit_repo = unit_repo;
    }

    public void AddUnit()
    {
        if (!m_inventory.TryAdd(m_unit.Code))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"<color=red>{m_unit.name}를 획득하는 과정에서 이미 동일한 영웅이 인벤토리에 존재함을 확인했습니다.</color>");
#endif
        }
    }
    #endregion Helper Methods
}
