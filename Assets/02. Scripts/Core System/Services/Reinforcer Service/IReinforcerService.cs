using System;

namespace ReinforcerService
{
    public interface IReinforcerService : ISaveable
    {
        event Action<ReinforcementType, int> OnUpdatedReinforcement;
        
        int GetField(ReinforcementType type);
        void UpgradeField(ReinforcementType type, int amount = 1);
    }
}