public interface ISalvationView
{
    void Inject(SalvationPresenter presenter);

    void UseUI(float target_time, float spawn_interval, int atk);
    void CoolUI(float cooldown_time);
}