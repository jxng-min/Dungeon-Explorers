public interface ICostView
{
    void Updates();

    void StartUI(float interval);
    void UpdateUI(float current_cost, float max_cost);
    void UpdateCost(int cost);
}