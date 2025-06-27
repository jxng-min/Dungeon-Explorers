using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MenuSetterView : MonoBehaviour, ISetterView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("UI 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    [Header("배경음 토글")]
    [SerializeField] private Toggle m_bgm_toggle;

    [Header("배경음 슬라이더")]
    [SerializeField] private Slider m_bgm_slider;

    [Header("효과음 토글")]
    [SerializeField] private Toggle m_sfx_toggle;

    [Header("효과음 슬라이더")]
    [SerializeField] private Slider m_sfx_slider;

    [Header("게임 종료 버튼")]
    [SerializeField] private Button m_exit_button;

    private Animator m_animator;
    private SetterPresenter m_presenter;
    private ISettingService m_setting_system;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_setting_system = ServiceLocator.Instance.SettingService;

        m_presenter = new SetterPresenter(this, m_setting_system);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);

        m_bgm_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnClickedBGMToggle(isOn));
        m_sfx_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnClickedSFXToggle(isOn));

        m_bgm_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedBGMRate(value));
        m_sfx_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedSFXRate(value));

        m_exit_button.onClick.AddListener(m_presenter.OnClickedGameExit);
    }

    #region Helper Methods
    public void OpenUI()
    {
        m_animator.SetBool("Open", true);

        m_presenter.Updates();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void UpdateUI(bool bgm, float bgm_value, bool sfx, float sfx_value)
    {
        m_bgm_toggle.isOn = bgm;
        m_bgm_slider.value = bgm_value;
        m_bgm_slider.interactable = bgm;

        m_sfx_toggle.isOn = sfx;
        m_sfx_slider.value = sfx_value;
        m_sfx_slider.interactable = sfx;
    }

    public void SetBGMInteractable(bool interactable)
    {
        m_bgm_slider.interactable = interactable;
    }

    public void SetSFXInteractable(bool interactable)
    {
        m_sfx_slider.interactable = interactable;
    }
    #endregion Helper Methods
}
