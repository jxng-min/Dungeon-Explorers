using System.Collections;

public interface IUltimateView
{
    void UseUI(float target_time, float spawn_interval, int atk);
    void CoolUI(float cooldown_time);
}