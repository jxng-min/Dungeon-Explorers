public interface ISettingView
{
    void Inject(SettingPresenter presenter);


    void Initialize(bool bgm_active, float bgm_rate, bool sfx_active, float sfx_rate);
    void OpenUI();
    void CloseUI();

    void ToggleBGMRateHandle(bool isOn);
    void ToggleSFXRateHandle(bool isOn);
}