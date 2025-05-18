using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SalvationCtrl : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("구원 버튼")]
    [SerializeField] private Button m_salvation_button;

    [Header("구원 버튼의 쿨타임 이미지")]
    [SerializeField] private Image m_cooltime_image;
    #endregion

    private float m_default_damage = 100f;
    private float m_default_duration = 10f;
    private float m_default_interval = 1f;
    private float m_default_cooltime = 60f;

    private float m_current_damage;
    private float m_current_duration;
    private float m_current_interval;
    private float m_current_cooltime;

    private void Awake()
    {
        m_current_damage = m_default_damage + (DataManager.Instance.Data.Reinforcement.SkillDamage - 1);
        m_current_duration = m_default_duration;
        m_current_interval = m_default_interval - 0.25f * (DataManager.Instance.Data.Reinforcement.SkillInterval - 1);
        m_current_cooltime = m_default_cooltime - 0.25f * (DataManager.Instance.Data.Reinforcement.SkillCooltime - 1);
    }

    public void BUTTON_Use()
    {
        StartCoroutine(Co_SpawnMeteor(m_current_duration, m_current_interval));
        StartCoroutine(Co_CoolDownSkill(m_current_cooltime));
    }

    private Vector2 GetMeteorPosition()
    {
        var camera = Camera.main;

        Vector3 bottom_left = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 top_right = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        float random_x = Random.Range(bottom_left.x, top_right.x);
        float fixed_y = top_right.y + 1f;

        return new Vector2(random_x, fixed_y);
    }

    private IEnumerator Co_SpawnMeteor(float target_time, float spawn_time)
    {
        float elapsed_time = 0f;
        float spawn_timer = 0f;

        while (elapsed_time <= target_time)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;
            spawn_timer += Time.deltaTime;

            if (spawn_timer >= spawn_time)
            {
                float random_atk = Random.Range(m_current_damage - 10f, m_current_damage + 10f);

                var obj = ObjectManager.Instance.GetObject(ObjectType.METEOR);
                obj.transform.position = GetMeteorPosition();

                var meteor = obj.GetComponent<Meteor>();
                meteor.Initialize(random_atk, 10f);

                spawn_timer = 0f;
            }
            yield return null;
        }
    }

    private IEnumerator Co_CoolDownSkill(float cooltime)
    {
        float elapsed_time = 0f;

        m_salvation_button.interactable = false;
        m_cooltime_image.fillAmount = 1f;

        while (elapsed_time <= cooltime)
        {
            // TODO: WaitUntil()로 PLAYING일 때까지 대기

            elapsed_time += Time.deltaTime;
            yield return null;

            float delta = elapsed_time / cooltime;
            m_cooltime_image.fillAmount = 1f - delta;
        }

        m_cooltime_image.fillAmount = 0f;
        m_salvation_button.interactable = true;
    }
}
