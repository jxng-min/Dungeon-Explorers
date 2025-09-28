public interface IStatusView
{
    void Inject(StatusPresenter presenter);

    void UpdateLevel(int level, float exp_rate);
    void UpdateMoney(int money);
}