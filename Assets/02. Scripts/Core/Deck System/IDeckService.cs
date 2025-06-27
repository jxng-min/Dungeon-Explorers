using System.Collections.Generic;

namespace DeckService
{
    public interface IDeckService
    {
        void Load();
        void Save();
        List<UnitCode> GetDeck();
        void SetDeck(int index, UnitCode code);
    }
}