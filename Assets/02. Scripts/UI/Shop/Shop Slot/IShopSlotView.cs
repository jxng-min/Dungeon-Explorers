using InventoryService;

public interface IShopSlotView
{
    void Initialize(IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit);
    void UpdateUI(bool has_unit, int money, int cost);
    void Updates();
    void Purchase();
}