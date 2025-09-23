public interface ISpawnView
{
    void Inject(SpawnPresenter presenter);

    void StartWave();
    void InstantiateEnemy(Wave wave);
}