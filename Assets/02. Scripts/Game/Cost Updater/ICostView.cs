public interface ICostView
{
    int GetCost();

    void StartUI();
    void UpdateUI(float current_cost, float max_cost);
    void UpdateCost(int cost);
}