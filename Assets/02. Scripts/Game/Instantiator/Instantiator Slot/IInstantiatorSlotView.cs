using Units;
using UnityEngine;

public interface IInstantiatorSlotView
{
    void Initialize(UnitCode code, UnitDataBase unit_db);

    void ClearUI();
    void InitUI(Sprite unit_sprite, int cost);
    void CoolUI(float target_time);
    void UpdateUI();
    void ToggleUI(bool active, float unit_cost);
}