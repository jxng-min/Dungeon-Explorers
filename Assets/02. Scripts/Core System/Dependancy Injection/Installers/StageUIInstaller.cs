using UnityEngine;
using UserService;

public class StageUIInstaller : MonoBehaviour, IInstaller
{
    [Header("스테이지 뷰")]
    [SerializeField] private StageView m_stage_view;

    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    public void Install()
    {
        InstallStageDataBase();
        InstallStage();
    }

    private void InstallStageDataBase()
    {
        DIContainer.Register<IStageDataBase>(m_stage_db);
    }

    private void InstallStage()
    {
        DIContainer.Register<IStageView>(m_stage_view);

        var stage_presenter = new StagePresenter(m_stage_view,
                                                 m_stage_db,
                                                 ServiceLocator.Get<IUserService>());
        DIContainer.Register<StagePresenter>(stage_presenter);
    }
}
