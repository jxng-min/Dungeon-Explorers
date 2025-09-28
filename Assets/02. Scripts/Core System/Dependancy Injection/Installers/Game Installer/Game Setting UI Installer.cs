using UnityEngine;

public class GameSettingUIInstaller : MonoBehaviour, IInstaller
{
    [Header("설정 뷰")]
    [SerializeField] private GameSettingView m_setting_view;

    public void Install()
    {
        InstallSetting();
    }

    private void InstallSetting()
    {
        DIContainer.Register<ISettingView>(m_setting_view);

        var setting_presenter = new SettingPresenter(m_setting_view,
                                                     ServiceLocator.Get<ISettingService>());
        DIContainer.Register<SettingPresenter>(setting_presenter);
    }
}
