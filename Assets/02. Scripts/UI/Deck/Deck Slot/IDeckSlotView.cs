using DeckService;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDeckSlotView : IPointerClickHandler
{
    void Initialize(UnitDataBase unit_db, IDeckService deck_system, ISelectorView selector_view, UnitCode code, bool is_candidate);
    void Updates();
    void UpdateUI(Sprite unit_sprite, int cost, bool is_selected);
    void Clear();
    void ClearUI();
    void SetHighlight(bool flag);
}