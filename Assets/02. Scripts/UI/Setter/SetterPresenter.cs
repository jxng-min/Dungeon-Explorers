public class SetterPresenter
{
    #region Variables
    private readonly ISetterView m_view;
    private SettingDataBase m_model;
    #endregion Variables

    public SetterPresenter(ISetterView view, SettingDataBase model)
    {
        m_view = view;
        m_model = model;
    }

    #region Helper Methods
    public void Updates()
    {
        m_view.UpdateUI(m_model.BGM, m_model.BGMRate, m_model.SFX, m_model.SFXRate);
    }

    public void OnClickedOpenUI()
    {
        m_view.OpenUI();
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }

    public void OnClickedBGMToggle(bool is_on)
    {
        m_model.BGM = is_on;

        m_view.SetBGMInteractable(is_on);
        // 사운드 매니저 처리
    }

    public void OnClickedSFXToggle(bool is_on)
    {
        m_model.SFX = is_on;

        m_view.SetSFXInteractable(is_on);
    }

    public void OnValueChangedBGMRate(float value)
    {
        m_model.BGMRate = value;
    }

    public void OnValueChangedSFXRate(float value)
    {
        m_model.SFXRate = value;
    }

    public void OnClickedLoadTitle()
    {
        LoadingManager.Instance.LoadScene("Title");
    }

    public void OnClickedGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion Helper Methods
}
