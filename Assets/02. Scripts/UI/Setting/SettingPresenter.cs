public class SettingPresenter
{
    private readonly ISettingView m_view;
    private readonly ISettingService m_setting_service;

    public SettingPresenter(ISettingView view,
                            ISettingService setting_service)
    {
        m_view = view;
        m_setting_service = setting_service;

        m_view.Inject(this);
    }

    public void OpenUI()
    {
        m_view.Initialize(m_setting_service.BGM,
                          m_setting_service.BGMRate,
                          m_setting_service.SFX,
                          m_setting_service.SFXRate);
        m_view.OpenUI();
    }

    public void CloseUI()
    {
        m_view.CloseUI();
    }

    public void OnValueChangedBGM(bool isOn)
    {
        m_setting_service.BGM = isOn;
        m_view.ToggleBGMRateHandle(isOn);
    }

    public void OnValueChangedBGMRate(float value)
    {
        m_setting_service.BGMRate = value;
    }

    public void OnValueChangedSFX(bool isOn)
    {
        m_setting_service.SFX = isOn;
        m_view.ToggleSFXRateHandle(isOn);
    }

    public void OnValueChangedSFXRate(float value)
    {
        m_setting_service.SFXRate = value;
    }

    public void OnClickedExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}