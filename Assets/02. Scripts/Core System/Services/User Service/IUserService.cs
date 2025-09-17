using System;

namespace UserService
{
    public interface IUserService : ISaveable
    {
        int LV { get; }
        int EXP { get; }
        int Stage { get; }

        event Action<int, int> OnUpdatedLevel;
        event Action<int> OnUpdatedStage;

        void Initialize();
        void UpdateLevel(int exp);
        void UpdateStage(int stage);
    }
}