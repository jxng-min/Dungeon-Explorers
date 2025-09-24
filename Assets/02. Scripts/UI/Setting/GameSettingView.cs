using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameSettingView : MonoBehaviour, ISettingView
{
    [Header("배경음 토글")]
    [SerializeField] private Toggle m_bgm_toggle;

    [Header("배경음 슬라이더")]
    [SerializeField] private Slider m_bgm_slider;

    [Header("효과음 토글")]
    [SerializeField] private Toggle m_sfx_toggle;

    [Header("효과음 슬라이더")]
    [SerializeField] private Slider m_sfx_slider;

    [Header("다시 시작 버튼")]
    [SerializeField] private Button m_retry_button;

    [Header("메인 화면 버튼")]
    [SerializeField] private Button m_title_button;

    [Header("열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;

    private SettingPresenter m_presenter;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        m_bgm_toggle.onValueChanged.RemoveListener((isOn) => m_presenter.OnValueChangedBGM(isOn));
        m_bgm_slider.onValueChanged.RemoveListener((value) => m_presenter.OnValueChangedBGMRate(value));
        
        m_sfx_toggle.onValueChanged.RemoveListener((isOn) => m_presenter.OnValueChangedSFX(isOn));
        m_sfx_slider.onValueChanged.RemoveListener((value) => m_presenter.OnValueChangedSFXRate(value));

        m_retry_button.onClick.RemoveListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.RemoveListener(m_presenter.OnClickedTitle);

        m_open_button.onClick.RemoveListener(m_presenter.OpenUI);
        m_close_button.onClick.RemoveListener(m_presenter.CloseUI);  
    }

    public void Inject(SettingPresenter presenter)
    {
        m_presenter = presenter;

        m_bgm_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnValueChangedBGM(isOn));
        m_bgm_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedBGMRate(value));
        
        m_sfx_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnValueChangedSFX(isOn));
        m_sfx_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedSFXRate(value));

        m_retry_button.onClick.AddListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.AddListener(m_presenter.OnClickedTitle);

        m_open_button.onClick.AddListener(m_presenter.OpenUI);
        m_close_button.onClick.AddListener(m_presenter.CloseUI);  
    }

    public void Initialize(bool bgm_active, 
                           float bgm_rate, 
                           bool sfx_active, 
                           float sfx_rate)
    {
        m_bgm_toggle.isOn = bgm_active;
        m_bgm_slider.value = bgm_rate;
        m_presenter.OnValueChangedBGM(bgm_active);

        m_sfx_toggle.isOn = sfx_active;
        m_sfx_slider.value = sfx_rate;
        m_presenter.OnValueChangedSFX(sfx_active);
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);

        GameEventBus.Publish(GameEventType.PAUSE);
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }

    public void ToggleBGMRateHandle(bool isOn)
    {
        m_bgm_slider.interactable = isOn;
    }

    public void ToggleSFXRateHandle(bool isOn)
    {
        m_sfx_slider.interactable = isOn;
    }
}
