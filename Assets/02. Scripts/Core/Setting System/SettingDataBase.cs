using UnityEngine;

[CreateAssetMenu(fileName = "New Setting Data", menuName = "SO/DB/Create SettingDataBase")]
public class SettingDataBase : ScriptableObject
{
    [Header("배경음 출력의 여부")]
    [SerializeField] private bool m_can_play_bgm = true;
    public bool BGM
    {
        get => m_can_play_bgm;
        set => m_can_play_bgm = value;
    }

    [Header("배경음의 크기")]
    [Range(0f, 1f)][SerializeField] private float m_bgm_rate = 0.5f;
    public float BGMRate
    {
        get => m_bgm_rate;
        set => m_bgm_rate = value;
    }

    [Header("효과음 출력의 여부")]
    [SerializeField] private bool m_can_play_sfx = true;
    public bool SFX
    {
        get => m_can_play_sfx;
        set => m_can_play_sfx = value;
    }

    [Header("효과음의 크기")]
    [Range(0f, 1f)][SerializeField] private float m_sfx_rate = 0.5f;
    public float SFXRate
    {
        get => m_sfx_rate;
        set => m_sfx_rate = value;
    }
}
