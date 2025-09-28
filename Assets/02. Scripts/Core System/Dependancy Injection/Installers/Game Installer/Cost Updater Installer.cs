using ReinforcerService;
using UnityEngine;

public class CostUpdaterInstaller : MonoBehaviour, IInstaller
{
    [Header("코스트 업데이터 뷰")]
    [SerializeField] private CostView m_cost_view;

    public void Install()
    {
        InstallCostUpdater();
        InjectCostUpdater();
    }

    private void InstallCostUpdater()
    {
        DIContainer.Register<ICostView>(m_cost_view);

        var cost_model = new CostModel(ServiceLocator.Get<IReinforcerService>(),
                                       DIContainer.Resolve<IntervalPresenter>());
        DIContainer.Register<CostModel>(cost_model);

        var cost_presenter = new CostPresenter(m_cost_view,
                                               cost_model);
        DIContainer.Register<CostPresenter>(cost_presenter);
    }

    private void InjectCostUpdater()
    {
        var interval_presenter = DIContainer.Resolve<IntervalPresenter>();

        interval_presenter.Inject(DIContainer.Resolve<CostPresenter>());
    }
}
