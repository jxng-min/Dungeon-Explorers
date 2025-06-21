using UnitRepository;
using EXPService;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public IUnitRepository UnitRepoService { get; private set; }
    public IEXPService EXPService { get;  private set; }

    private void OnEnable()
    {
        UnitRepoService = new LocalUnitRepository();
        EXPService = new LocalEXPSystem();
    }

    private void OnDisable()
    {
        UnitRepoService = null;
        EXPService = null;
    }
}
