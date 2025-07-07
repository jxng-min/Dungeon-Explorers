using Units;
using UnityEngine;

public interface ISelectorView
{
    void Initialize(IDeckSlotView deck_slot, Unit unit, Vector2 touch_position, bool is_candidate);
    void OpenUI(Vector2 touch_position, bool is_candidate);
    void CloseUI();
    void SetHightlightSlots(bool flag);
}