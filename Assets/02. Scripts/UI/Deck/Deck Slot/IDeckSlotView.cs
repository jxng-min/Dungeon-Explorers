using DeckService;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDeckSlotView : IPointerClickHandler
{
    void Initialize(UnitDataBase unit_db, IDeckService deck_system, IDeckView deck_view, ISelectorView selector_view, UnitCode code);

    void Updates();
    void UpdateUI(Sprite unit_sprite, int cost, bool is_selected);
    void Clear();
    void ClearUI();

    void Swap(UnitCode code);
    UnitCode GetCode();
    void SetHighlight(bool flag);
}