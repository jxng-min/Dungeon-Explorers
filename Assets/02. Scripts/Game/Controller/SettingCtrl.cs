using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SettingCtrl : MonoBehaviour
{
    [Header("배경음 토글")]
    [SerializeField] private Toggle m_bgm_toggle;

    [Header("효과음 토글")]
    [SerializeField] private Toggle m_sfx_toggle;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_bgm_toggle.isOn = SettingManager.Instance.Data.BGM;
        m_sfx_toggle.isOn = SettingManager.Instance.Data.SFX;
    }

    public void BUTTON_Open()
    {
        GameEventBus.Publish(GameEventType.PAUSE);

        m_animator.SetBool("Open", true);
    }

    public void BUTTON_Close()
    {
        GameEventBus.Publish(GameEventType.PLAYING);

        m_animator.SetBool("Open", false);
        SettingManager.Instance.SaveJson();
    }

    public void TOGGLE_BGM()
    {
        if (!m_bgm_toggle.isOn)
        {
            SettingManager.Instance.Data.BGM = false;

            SoundManager.Instance.BGM.Pause();
        }
        else
        {
            SettingManager.Instance.Data.BGM = true;

            if (SoundManager.Instance.BGM.clip == null || SoundManager.Instance.BGM.clip.name != LoadingManager.Instance.Scene)
            {
                switch (LoadingManager.Instance.Scene)
                {
                    case "Login":
                        SoundManager.Instance.PlayBGM("Login");
                        break;

                    case "Title":
                        SoundManager.Instance.PlayBGM("Title");
                        break;

                    case "Game":
                        SoundManager.Instance.PlayBGM("Game");
                        break;
                }
            }
            else
            {
                SoundManager.Instance.BGM.UnPause();
            }
        }
    }

    public void TOGGLE_SFX()
    {
        SettingManager.Instance.Data.SFX = m_sfx_toggle.isOn;
    }

    public void BUTTON_Retry()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void BUTTON_Title()
    {
        LoadingManager.Instance.LoadScene("Title");
    }

    public void BUTTON_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
