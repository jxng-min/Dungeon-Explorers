using System.Collections;
using ObjectPool;
using ReinforcerService;
using UnityEngine;
using UnityEngine.UI;

public class UltimateView : MonoBehaviour, IUltimateView
{
    #region Variables
    [Header("궁극 스킬 버튼")]
    [SerializeField] private Button m_ultimate_button;

    [Header("궁극 스킬 버튼의 쿨타임 이미지")]
    [SerializeField] private Image m_cooldown_image;

    private UltimatePresenter m_presenter;
    private IReinforcerService m_reinforcement_system;
    #endregion Variables

    private void Awake()
    {
        m_reinforcement_system = ServiceLocator.Get<IReinforcerService>();

        m_presenter = new UltimatePresenter(this, m_reinforcement_system);

        m_ultimate_button.onClick.AddListener(m_presenter.OnClickedUseButton);
    }

    #region Helper Methods
    public void UseUI(float target_time, float spawn_interval, int atk)
    {
        StartCoroutine(Co_UseSkill(target_time, spawn_interval, atk));
    }

    public void CoolUI(float cooldown_time)
    {
        StartCoroutine(Co_CooldownSkill(cooldown_time));
    }

    private IEnumerator Co_UseSkill(float target_time, float spawn_interval, int atk)
    {
        float elapsed_time = 0f;
        float spawn_timer = 0f;

        while (elapsed_time <= target_time)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;
            spawn_timer += Time.deltaTime;

            if (spawn_timer >= spawn_interval)
            {
                var obj = ObjectManager.Instance.GetObject(ObjectType.METEOR);
                obj.transform.position = GetMeteorPosition();

                var meteor = obj.GetComponent<Meteor>();
                meteor.Initialize(atk, 10f);

                spawn_timer = 0f;
            }

            yield return null;
        }
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

    private IEnumerator Co_CooldownSkill(float cooldown_time)
    {
        m_ultimate_button.interactable = false;
        m_cooldown_image.fillAmount = 1f;

        float elapsed_time = 0f;
        while (elapsed_time <= cooldown_time)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;

            float delta = elapsed_time / cooldown_time;
            m_cooldown_image.fillAmount = 1f - delta;

            yield return null;
        }

        m_cooldown_image.fillAmount = 0f;
        m_ultimate_button.interactable = true;
    }
    #endregion Helper Methods
}
