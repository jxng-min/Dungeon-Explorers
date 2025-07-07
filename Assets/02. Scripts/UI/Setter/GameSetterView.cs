using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameSetterView : MonoBehaviour, ISetterView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("배경음 토글")]
    [SerializeField] private Toggle m_bgm_toggle;

    [Header("효과음 토글")]
    [SerializeField] private Toggle m_sfx_toggle;

    [Header("다시하기 버튼")]
    [SerializeField] private Button m_retry_button;

    [Header("타이틀 버튼")]
    [SerializeField] private Button m_title_button;

    [Header("UI 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private SetterPresenter m_presenter;
    private ISettingService m_setting_system;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_setting_system = ServiceLocator.Get<ISettingService>();

        m_presenter = new SetterPresenter(this, m_setting_system);

        m_retry_button.onClick.AddListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.AddListener(m_presenter.OnClickedLoadTitle);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
        GameEventBus.Publish(GameEventType.PAUSE);

        m_presenter.Updates();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
        GameEventBus.Publish(GameEventType.PLAYING);
    }
    
    public void UpdateUI(bool bgm, float bgm_value, bool sfx, float sfx_value)
    {
        m_bgm_toggle.isOn = bgm;
        m_sfx_toggle.isOn = sfx;
    }

    public void SetBGMInteractable(bool interactable){}

    public void SetSFXInteractable(bool interactable){}
}