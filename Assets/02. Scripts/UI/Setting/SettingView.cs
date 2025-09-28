using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour, ISettingView
{
    [Header("UI 관련 컴포넌트")]
    [Header("캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("배경음 토글")]
    [SerializeField] private Toggle m_bgm_toggle;

    [Header("배경음 슬라이더")]
    [SerializeField] private Slider m_bgm_slider;

    [Header("효과음 토글")]
    [SerializeField] private Toggle m_sfx_toggle;

    [Header("효과음 슬라이더")]
    [SerializeField] private Slider m_sfx_slider;

    [Header("열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("닫기 버튼")]
    [SerializeField] private Button m_close_button;

    [Header("종료 버튼")]
    [SerializeField] private Button m_exit_button;

    private Coroutine m_toggle_coroutine;

    private SettingPresenter m_presenter;

    private void OnDestroy()
    {
        m_bgm_toggle.onValueChanged.RemoveListener((isOn) => m_presenter.OnValueChangedBGM(isOn));
        m_bgm_slider.onValueChanged.RemoveListener((value) => m_presenter.OnValueChangedBGMRate(value));
        
        m_sfx_toggle.onValueChanged.RemoveListener((isOn) => m_presenter.OnValueChangedSFX(isOn));
        m_sfx_slider.onValueChanged.RemoveListener((value) => m_presenter.OnValueChangedSFXRate(value));

        m_open_button.onClick.RemoveListener(m_presenter.OpenUI);
        m_close_button.onClick.RemoveListener(m_presenter.CloseUI);

        m_exit_button.onClick.RemoveListener(m_presenter.OnClickedExit);
    }

    public void Inject(SettingPresenter presenter)
    {
        m_presenter = presenter;

        m_bgm_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnValueChangedBGM(isOn));
        m_bgm_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedBGMRate(value));
        
        m_sfx_toggle.onValueChanged.AddListener((isOn) => m_presenter.OnValueChangedSFX(isOn));
        m_sfx_slider.onValueChanged.AddListener((value) => m_presenter.OnValueChangedSFXRate(value));

        m_open_button.onClick.AddListener(m_presenter.OpenUI);
        m_close_button.onClick.AddListener(m_presenter.CloseUI);

        m_exit_button.onClick.AddListener(m_presenter.OnClickedExit);
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
        ToggleCoroutine(true);
    }

    public void CloseUI()
    {
        ToggleCoroutine(false);
    }

    public void ToggleBGMRateHandle(bool isOn)
    {
        m_bgm_slider.interactable = isOn;
    }

    public void ToggleSFXRateHandle(bool isOn)
    {
        m_sfx_slider.interactable = isOn;
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }

    private void ToggleCoroutine(bool is_open)
    {
        if(m_toggle_coroutine != null)
        {
            StopCoroutine(m_toggle_coroutine);
            m_toggle_coroutine = null;
        }

        m_toggle_coroutine = StartCoroutine(Co_ToggleUI(is_open));
    }

    private IEnumerator Co_ToggleUI(bool is_open)
    {
        m_canvas_group.blocksRaycasts = is_open;
        m_canvas_group.interactable = is_open;

        float elapsed_time = 0f;
        float target_time = 0.5f;

        if(is_open && m_canvas_group.alpha >= 0.9f)
        {
            yield break;
        }

        if(!is_open && m_canvas_group.alpha <= 0.1f)
        {
            yield break;
        }

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            var alpha_delta = elapsed_time / target_time; 
            m_canvas_group.alpha = is_open ? alpha_delta : 1f - alpha_delta;

            yield return null;
        }

        m_canvas_group.alpha = is_open ? 1f : 0f;
    }
}
