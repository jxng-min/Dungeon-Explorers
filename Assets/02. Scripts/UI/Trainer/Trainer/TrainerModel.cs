using InventoryService;

public class TrainerModel
{
    #region Variables
    private IInventoryService m_inventory_system;
    #endregion Variables

    #region Properties
    public IInventoryService InventorySystem { get => m_inventory_system; }
    #endregion Properties

    public TrainerModel(IInventoryService inventory_system)
    {
        m_inventory_system = inventory_system;
    }
}
