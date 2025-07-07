using InventoryService;
using ReinforcementService;

public interface IReinforcementSlotView
{
    void Initialize(ReinforceDataBase db, IReinforcementService reinforce_service, IInventoryService inventory_service, ReinforcementType type);
    void Updates();
    void UpdateUI(int level, int money, int cost);
}
