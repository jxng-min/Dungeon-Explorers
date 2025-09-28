using ReinforcerService;
using UnityEngine;

public class SalvationUpdaterInstaller : MonoBehaviour, IInstaller
{
    [Header("구원 뷰")]
    [SerializeField] private SalvationView m_salvation_view;

    public void Install()
    {
        InstallSalvation();
    }

    private void InstallSalvation()
    {
        DIContainer.Register<ISalvationView>(m_salvation_view);

        var salvation_model = new SalvationModel(ServiceLocator.Get<IReinforcerService>());
        DIContainer.Register<SalvationModel>(salvation_model);

        var salvation_presenter = new SalvationPresenter(m_salvation_view,
                                                         salvation_model);
        DIContainer.Register<SalvationPresenter>(salvation_presenter);
    }
}
