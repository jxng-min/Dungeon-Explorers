using System.Collections.Generic;

namespace ReinforcementService
{
    public interface IReinforcementService
    {
        Dictionary<ReinforcementType, int> GetDict();
        int GetField(ReinforcementType type);
        void UpgradeField(ReinforcementType type, int amount = 1);
        void Load();
        void Save();
    }
}