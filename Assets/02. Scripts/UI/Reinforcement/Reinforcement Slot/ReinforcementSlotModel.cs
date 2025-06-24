using InventoryService;
using ReinforcementService;

public class ReinforcementSlotModel
{
    #region Variables
    private IReinforcementService m_reinforce_system;
    private IInventoryService m_inventory_system;
    private ReinforcementType m_type;

    private const int DEFAULT_COST = 50;
    private const int GROWTH_COST = 10;
    #endregion Variables

    #region Helper Methods
    public void Initialize(IReinforcementService reinforcement_system, IInventoryService inventory_service, ReinforcementType type)
    {
        m_reinforce_system = reinforcement_system;
        m_inventory_system = inventory_service;
        m_type = type;
    }

    public int GetField()
    {
        return m_reinforce_system.GetField(m_type);
    }

    public void UpgradeField(int amount = 1)
    {
        m_reinforce_system.UpgradeField(m_type, amount);
    }

    public int GetMoney()
    {
        return m_inventory_system.Money;
    }

    public void UpdateMoney(int amount)
    {
        m_inventory_system.Money += amount;
    }

    public int GetCost()
    {
        return DEFAULT_COST + (GROWTH_COST * m_reinforce_system.GetField(m_type));
    }
    #endregion Helper Methods
}
