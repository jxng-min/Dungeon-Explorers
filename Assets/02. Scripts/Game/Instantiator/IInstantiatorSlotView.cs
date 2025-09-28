using UnityEngine;

public interface IInstantiatorSlotView
{
    void Inject(InstantiatorSlotPresenter presenter);

    void ClearUI();
    void InitUI(Sprite unit_sprite, int cost);
    void CoolUI(float target_time);
    void UpdateUI(bool active, float unit_cost);
}