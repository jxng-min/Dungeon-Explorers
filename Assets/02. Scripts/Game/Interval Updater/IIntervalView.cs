public interface IIntervalView
{
    void Inject(IntervalPresenter presenter);

    void UpdateUI(bool active, int upgrade_cost);
}