using UnitRepository;
using EXPService;
using InventoryService;
using UserDataService;
using UnityEngine;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public IUnitRepository UnitRepoService { get; private set; }
    public IEXPService EXPService { get; private set; }
    public IInventoryService InvenService { get; private set; }
    public IUserDataService UserDataService { get; private set; }

    private void OnEnable()
    {
        UnitRepoService = new LocalUnitRepository();
        EXPService = new LocalEXPSystem();
        InvenService = new LocalInventory();
        UserDataService = new LocalUserDataSystem();
    }

    private void OnDisable()
    {
        UnitRepoService = null;
        EXPService = null;
        InvenService = null;
        UserDataService = null;
    }
}