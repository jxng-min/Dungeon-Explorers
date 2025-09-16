namespace UnitService
{
    public interface IUnitService
    {
        string GetName(UnitCode code);
        string GetDescription(UnitCode code);
    }
}