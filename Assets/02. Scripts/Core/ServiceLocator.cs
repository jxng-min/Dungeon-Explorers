using UnitRepository;
using EXPService;
using InventoryService;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public IUnitRepository UnitRepoService { get; private set; }
    public IEXPService EXPService { get; private set; }
    public IInventoryService InvenService { get; private set; }

    private void OnEnable()
    {
        UnitRepoService = new LocalUnitRepository();
        EXPService = new LocalEXPSystem();
        InvenService = new LocalInventory();
    }

    private void OnDisable()
    {
        UnitRepoService = null;
        EXPService = null;
        InvenService = null;
    }
}
