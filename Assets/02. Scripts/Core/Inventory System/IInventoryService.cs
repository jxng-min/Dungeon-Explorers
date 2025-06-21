namespace InventoryService
{
    public interface IInventoryService
    {
        public bool HasUnit(UnitCode code);
        public bool TryAdd(UnitCode code, int upgrade_count = 1);
        public Unit GetUnit(UnitCode code);
        public void Load();
        public void Save();
    }
}