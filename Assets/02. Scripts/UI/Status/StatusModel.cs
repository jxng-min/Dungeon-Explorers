using EXPService;
using InventoryService;
using UserDataService;

public class StatusModel
{
    #region Variables
    private IUserDataService m_user_data_system;
    private IInventoryService m_inventory_system;
    private IEXPService m_exp_system;
    #endregion Variables

    #region Properties
    public IUserDataService UserDataSystem { get => m_user_data_system; }
    public IInventoryService InventorySystem { get => m_inventory_system; }
    public IEXPService EXPSystem { get => m_exp_system; }
    #endregion Properties

    public StatusModel(IUserDataService user_data_system, IInventoryService inventory_service, IEXPService exp_system)
    {
        m_user_data_system = user_data_system;
        m_inventory_system = inventory_service;
        m_exp_system = exp_system;
    }
}
