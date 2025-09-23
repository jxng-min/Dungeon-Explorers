using UnityEngine;

public class SpawnerInstaller : MonoBehaviour, IInstaller
{
    [Header("스포너 뷰")]
    [SerializeField] private SpawnView m_spawn_view;

    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    public void Install()
    {
        InstallStageDataBase();
        InstallSpawner();
    }

    private void InstallStageDataBase()
    {
        DIContainer.Register<IStageDataBase>(m_stage_db);
    }

    private void InstallSpawner()
    {
        DIContainer.Register<ISpawnView>(m_spawn_view);

        var spawn_presenter = new SpawnPresenter(m_spawn_view,
                                                 m_stage_db);
        DIContainer.Register<SpawnPresenter>(spawn_presenter);
    }
}
