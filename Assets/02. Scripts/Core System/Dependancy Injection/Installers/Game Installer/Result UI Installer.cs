using InventoryService;
using UnityEngine;
using UserService;

public class ResultUIInstaller : MonoBehaviour, IInstaller
{
    [Header("결과 뷰")]
    [SerializeField] private ResultView m_result_view;

    [Header("아군 타워")]
    [SerializeField] private TowerUnit m_hero_tower;

    [Header("적군 타워")]
    [SerializeField] private TowerUnit m_enemy_tower;

    public void Install()
    {
        InstallResult();
    }

    private void InstallResult()
    {
        DIContainer.Register<IResultView>(m_result_view);

        var result_presenter = new ResultPresenter(m_result_view,
                                                   DIContainer.Resolve<IStageDataBase>(),
                                                   ServiceLocator.Get<IInventoryService>(),
                                                   ServiceLocator.Get<IUserService>());
        DIContainer.Register<ResultPresenter>(result_presenter);

        result_presenter.Inject(m_hero_tower,
                                m_enemy_tower);
    }
}
