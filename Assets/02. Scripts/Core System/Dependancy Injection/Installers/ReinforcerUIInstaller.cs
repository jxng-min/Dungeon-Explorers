using InventoryService;
using ReinforcerService;
using UnityEngine;

public class ReinforcerUIInstaller : MonoBehaviour, IInstaller
{
    [Header("능력치 강화 뷰")]
    [SerializeField] private ReinforcerView m_reinforcer_view;

    [Header("능력치 강화 데이터베이스")]
    [SerializeField] private ReinforcerDataBase m_reinforcer_db;

    public void Install()
    {
        InstallReinforcer();
    }

    private void InstallReinforcer()
    {
        DIContainer.Register<IReinforcerView>(m_reinforcer_view);

        var reinforcer_presenter = new ReinforcerPresenter(m_reinforcer_view,
                                                           ServiceLocator.Get<IInventoryService>(),
                                                           ServiceLocator.Get<IReinforcerService>(),
                                                           m_reinforcer_db);
        DIContainer.Register<ReinforcerPresenter>(reinforcer_presenter);
    }
}
