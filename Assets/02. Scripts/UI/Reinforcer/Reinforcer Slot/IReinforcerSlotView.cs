using UnityEngine;

public interface IReinforcerSlotView
{
    void Inject(ReinforcerSlotPresenter presenter);
    
    void UpdateUI(string reinforcement_name, Sprite reinforcement_image);
    void UpdateCost(int cost, bool can_purchase);
    void UpdateReinforcement(int level, bool is_limit);
    void PlaySFX(string sfx_name);
}