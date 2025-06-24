using System.Collections.Generic;

namespace InventoryService
{
    public interface IInventoryService
    {
        int Money { get; set; }
        List<Unit> Units { get; set; }

        bool HasUnit(UnitCode code);
        bool TryAdd(UnitCode code, int upgrade_count = 1);
        Unit GetUnit(UnitCode code);
        void Load();
        void Save();
    }
}