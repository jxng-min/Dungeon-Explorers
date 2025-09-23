using ReinforcerService;
using UnityEngine;

public class TowerInitializerInstaller : MonoBehaviour, IInstaller
{
    [Header("타워 초기화자 뷰")]
    [SerializeField] private TowerInitializerView m_tower_initializer_view;

    public void Install()
    {
        InstallTowerInitializer();
    }

    private void InstallTowerInitializer()
    {
        DIContainer.Register<ITowerInitializerView>(m_tower_initializer_view);

        var tower_initializer_presenter = new TowerInitializerPresenter(m_tower_initializer_view,
                                                                        ServiceLocator.Get<IReinforcerService>(),
                                                                        DIContainer.Resolve<IStageDataBase>());
        DIContainer.Register<TowerInitializerPresenter>(tower_initializer_presenter);
    }
}
