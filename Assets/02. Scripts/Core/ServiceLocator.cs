using UnitRepository;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public IUnitRepository UnitRepoService { get; private set; }

    private void OnEnable()
    {
        UnitRepoService = new LocalUnitRepository();
    }

    private void OnDisable()
    {
        UnitRepoService = null;
    }
}
