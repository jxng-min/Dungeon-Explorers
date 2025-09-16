using UnitService;
using UnityEngine;

public interface IInstantiatorSlotView
{
    void Initialize(UnitCode code, IUnitDataBase unit_db, ICostView cost_view);

    void ClearUI();
    void InitUI(Sprite unit_sprite, int cost);
    void CoolUI(float target_time);
    void UpdateUI();
    void ToggleUI(bool active, float unit_cost);
}