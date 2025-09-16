using System.Collections.Generic;

namespace UnitService
{
    public interface IUnitDataBase
    {
        List<Unit> GreenList { get; }
        List<Unit> RedList { get; }

        Unit GetUnit(UnitCode code);
    }
}