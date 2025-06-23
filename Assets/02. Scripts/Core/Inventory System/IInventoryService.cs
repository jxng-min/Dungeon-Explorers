using System.Collections.Generic;

namespace InventoryService
{
    public interface IInventoryService
    {
        public int Money { get; set; }
        public List<Unit> Units { get; set; }

        public bool HasUnit(UnitCode code);
        public bool TryAdd(UnitCode code, int upgrade_count = 1);
        public Unit GetUnit(UnitCode code);
        public void Load();
        public void Save();
    }
}