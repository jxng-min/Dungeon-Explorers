using UnityEngine;

public interface ICompactTrainerView
{
    void Inject(CompactTrainerPresenter presenter);

    void OpenUI();
    void UpdateUI(string unit_name, Sprite unit_sprite);
    void CloseUI();
    void UpdateLevel(int current_level, int max_level, bool is_limit);
    void UpdateCost(int cost, bool can_train);
}