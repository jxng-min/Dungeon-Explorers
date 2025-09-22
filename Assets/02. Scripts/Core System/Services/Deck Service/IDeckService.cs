using System;

namespace DeckService
{
    public interface IDeckService : ISaveable
    {
        event Action<int, UnitCode, UnitCode> OnUpdatedDeck;

        void Initialize();
        void SetDeck(int index, UnitCode code);
        bool HasDeck(UnitCode code);
        int GetIndex(UnitCode code);
    }
}