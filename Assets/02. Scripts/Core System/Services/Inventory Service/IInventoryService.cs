using System;
using System.Collections.Generic;

namespace InventoryService
{
    public interface IInventoryService : ISaveable
    {
        int Money { get; }
        List<UnitData> Units { get; }

        event Action<int> OnUpdatedMoney;
        event Action<UnitData> OnUpdatedUnit;

        void Initialize();

        bool HasUnit(UnitCode code);
        bool AddUnit(UnitCode code, int upgrade_count = 1);
        UnitData GetUnit(UnitCode code);

        void UpdateMoney(int amount);
    }
}