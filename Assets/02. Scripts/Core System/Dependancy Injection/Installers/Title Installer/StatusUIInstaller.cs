using EXPService;
using InventoryService;
using UnityEngine;
using UserService;

public class StatusUIInstaller : MonoBehaviour, IInstaller
{
    [Header("스테이터스 뷰")]
    [SerializeField] private StatusView m_status_view;

    public void Install()
    {
        InstallStatus();
    }

    private void InstallStatus()
    {
        DIContainer.Register<IStatusView>(m_status_view);

        var status_presenter = new StatusPresenter(m_status_view,
                                                   ServiceLocator.Get<IInventoryService>(),
                                                   ServiceLocator.Get<IUserService>(),
                                                   ServiceLocator.Get<IEXPService>());
        DIContainer.Register<StatusPresenter>(status_presenter);
    }
}
