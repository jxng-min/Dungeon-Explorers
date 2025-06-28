using InventoryService;
using Units;

public class TrainerInfoModel
{
    #region Variables
    private UnitDataBase m_unit_db;
    private IInventoryService m_inventory_system;
    private InventoryService.Unit m_unit;
    private IUnitRepository m_unit_repo;
    #endregion Variables

    #region Properties
    public float HP
    {
        get => m_unit_db.GetUnit(m_unit.Code).HP
                + (m_unit_db.GetUnit(m_unit.Code) as Hero).GrowthHP * m_inventory_system.GetUnit(m_unit.Code).Upgrade;
    }

    public int ATK
    {
        get => m_unit_db.GetUnit(m_unit.Code).ATK
                + (m_unit_db.GetUnit(m_unit.Code) as Hero).GrowthATK * m_inventory_system.GetUnit(m_unit.Code).Upgrade;
    }

    public int MaxUpgrade { get => (m_unit_db.GetUnit(m_unit.Code) as Hero).MaxUpgrade; }

    public int Cost
    {
        get => (m_unit_db.GetUnit(m_unit.Code) as Hero).UpgradeCost
                + 20 * m_inventory_system.GetUnit(m_unit.Code).Upgrade;
    }

    public string Name
    {
        get => m_unit_repo.GetName(m_unit.Code);
    }

    public int Upgrade
    {
        get => m_inventory_system.GetUnit(m_unit.Code).Upgrade;
        set => m_inventory_system.GetUnit(m_unit.Code).Upgrade = value;
    }

    public int Money
    {
        get => m_inventory_system.Money;
        set => m_inventory_system.Money = value;
    }
    #endregion Properties

    public TrainerInfoModel(UnitDataBase unit_db, IInventoryService inventory_system)
    {
        m_unit_db = unit_db;
        m_inventory_system = inventory_system;
    }

    #region Helper Methods
    public void Initialize(IUnitRepository unit_repo, InventoryService.Unit unit)
    {
        m_unit = unit;
        m_unit_repo = unit_repo;
    }
    #endregion Helper Methods
}
