using UnitRepository;
using EXPService;
using InventoryService;
using UserDataService;
using ReinforcementService;
using DeckService;
using SettingService;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public IUnitRepository UnitRepoService { get; private set; }
    public IEXPService EXPService { get; private set; }
    public IInventoryService InvenService { get; private set; }
    public IUserDataService UserDataService { get; private set; }
    public IReinforcementService ReinforceService { get; private set; }
    public IDeckService DeckService { get; private set; }
    public ISettingService SettingService { get; private set; }

    private void OnEnable()
    {
        UnitRepoService = new LocalUnitRepository();
        EXPService = new LocalEXPSystem();
        InvenService = new LocalInventory();
        UserDataService = new LocalUserDataSystem();
        ReinforceService = new LocalReinforcementSystem();
        DeckService = new LocalDeckSystem();
        SettingService = new LocalSettingSystem();
    }

    private void OnDisable()
    {
        UnitRepoService = null;
        EXPService = null;
        InvenService = null;
        UserDataService = null;
        ReinforceService = null;
        DeckService = null;
        SettingService = null;
    }
}