using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [Header("BGM 목록")]
    [SerializeField] private List<AudioClip> m_bgm_list;

    [Header("SFX 목록")]
    [SerializeField] private List<AudioClip> m_sfx_list;

    private AudioSource m_bgm_source;
    public AudioSource BGM { get => m_bgm_source; }

    private string m_last_bgm_name;
    public string LastBGM { get => m_last_bgm_name; }

    private new void Awake()
    {
        base.Awake();

        m_bgm_source = GetComponent<AudioSource>();
    }

    public void PlayBGM(string clip_name)
    {
        if (!SettingManager.Instance.Data.BGM)
        {
            return;
        }

        StartCoroutine(ChangeBGM(clip_name));
    }

    private IEnumerator ChangeBGM(string clip_name)
    {
        int target_index = -1;
        for (int i = 0; i < m_bgm_list.Count; i++)
        {
            if (m_bgm_list[i].name == clip_name)
            {
                target_index = i;
                break;
            }
        }

        if (target_index != -1)
        {
            if (BGM.isPlaying)
            {
                if (BGM.clip != null)
                {
                    m_last_bgm_name = BGM.clip.name;
                }

                yield return StartCoroutine(Fade(true));
            }

            BGM.clip = m_bgm_list[target_index];
            BGM.Play();

            yield return StartCoroutine(Fade(false));
        }
    }

    private IEnumerator Fade(bool is_out)
    {
        float elapsed_time = 0f;
        float target_time = 0.4f;

        while (elapsed_time <= target_time)
        {
            float time = elapsed_time / target_time;

            if (is_out)
            {
                BGM.volume = Mathf.Lerp(0.4f, 0f, time);
            }
            else
            {
                BGM.volume = Mathf.Lerp(0.4f, 0f, time);
            }

            elapsed_time += Time.deltaTime;
            yield return null;
        }

        if (is_out)
        {
            BGM.volume = 0f;
        }
        else
        {
            BGM.volume = 0.4f;
        }
    }

    public void PlaySFX(string clip_name)
    {
        int target_index = -1;
        for (int i = 0; i < m_sfx_list.Count; i++)
        {
            if (m_sfx_list[i].name == clip_name)
            {
                target_index = i;
                break;
            }
        }

        if (target_index != -1)
        {
            var effect_source = ObjectManager.Instance.GetObject(ObjectType.SFX).GetComponent<AudioSource>();

            if (SettingManager.Instance.Data.SFX)
            {
                effect_source.volume = Random.Range(0.7f, 1.0f);
                effect_source.pitch = Random.Range(0.8f, 1.1f);
            }
            else
            {
                effect_source.volume = 0f;
            }

            effect_source.clip = m_sfx_list[target_index];
            effect_source.Play();

            StartCoroutine(ReturnEffect(effect_source));
        }
    }

    private IEnumerator ReturnEffect(AudioSource target_source)
    {
        float elapsed_time = 0f;
        float target_time = target_source.clip.length;

        while (elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        ObjectManager.Instance.ReturnObject(target_source.gameObject, ObjectType.SFX);
    }
}
