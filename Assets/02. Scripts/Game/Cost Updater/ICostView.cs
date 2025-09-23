public interface ICostView
{
    void Inject(CostPresenter presenter);

    void StartUI();
    void UpdateUI(float current_cost, float max_cost);
}