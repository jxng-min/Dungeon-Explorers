using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageCtrl : MonoBehaviour
{
    [Header("캐릭터 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_character_slot_root;
    private Caller[] m_character_slots;

    [Header("생산량 상태 라벨")]
    [SerializeField] private TMP_Text m_cost_state_label;
    private int m_max_cost = 100;
    private int m_current_cost = 0;
    private float m_cost_interval = 0;

    [Header("타워 강화 버튼")]
    [SerializeField] private Button m_tower_button;

    [Header("타워 강화 라벨")]
    [SerializeField] private TMP_Text m_tower_label;
    private int m_tower_level = 0;
    private int m_tower_cost = 0;

    [Header("비상 스킬 버튼")]
    [SerializeField] private Button m_skill_button;

    [Header("비상 스킬 버튼의 쿨타임 이미지")]
    [SerializeField] private Image m_skill_filed_image;
    private float m_skill_atk;

    private void Awake()
    {
        m_character_slots = m_character_slot_root.GetComponentsInChildren<Caller>();
    }

    private void Start()
    {
        InitializeSlots();
        InitializeCost();

        StartCoroutine(Co_CostIncrease());
    }

    private void Update()
    {
        m_cost_state_label.text = $"{m_current_cost}/{m_max_cost}";

        UpdateSlots();
        UpdateTowerButton();
    }

    #region 슬롯 관리 관련 메서드
    private void InitializeSlots()
    {
        for (int i = 0; i < 5; i++)
        {
            m_character_slots[i].Initialize(StageManager.Instance.Party[i]);
        }
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < 5; i++)
        {
            if (StageManager.Instance.Party[i].ID >= 0)
            {
                m_character_slots[i].UpdateState(m_current_cost);
            }
        }
    }
    #endregion

    #region 비용 관련 메서드
    private void InitializeCost()
    {
        int default_max_cost = 100;
        m_max_cost = default_max_cost /* + 10 * 강화 레벨 */;

        float default_cost_interval = 0.2f;
        m_cost_interval = default_cost_interval - 0.01f * m_tower_level;
    }

    public void UpdateCurrentCost(int amount)
    {
        m_current_cost -= amount;
        if (m_current_cost < 0)
            m_current_cost = 0;
    }

    private IEnumerator Co_CostIncrease()
    {
        float elapsed_time = 0f;

        while (elapsed_time <= m_cost_interval)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        if (m_current_cost < m_max_cost)
        {
            m_current_cost++;
        }

        yield return StartCoroutine(Co_CostIncrease());
    }
    #endregion

    #region 타워 관련 메서드
    private void UpdateTowerButton()
    {
        int default_tower_cost = 10;
        m_tower_cost = default_tower_cost + (default_tower_cost * m_tower_level);

        if (m_current_cost >= m_tower_cost)
        {
            m_tower_button.interactable = true;
            m_tower_label.text = $"<color=green>{m_tower_cost}</color>";
        }
        else
        {
            m_tower_button.interactable = false;
            m_tower_label.text = $"<color=red>{m_tower_cost}</color>";
        }
    }

    public void BUTTON_UpgradeTower()
    {
        m_current_cost -= m_tower_cost;
        m_tower_level++;
    }
    #endregion

    #region 스킬 관련 메서드

    private void UseSkill(float duration, float interval)
    {
        StartCoroutine(Co_SpawnMeteor(duration, interval));
    }

    private void CoolDownSkill()
    {
        float default_cooltime = 40f;
        float final_cooltime = default_cooltime /* - 0.25 * 업그레이드 수 */;

        StartCoroutine(Co_CoolDownSkill(final_cooltime));
    }

    private IEnumerator Co_CoolDownSkill(float cooltime)
    {
        float elapsed_time = 0f;

        m_skill_button.interactable = false;
        m_skill_filed_image.fillAmount = 1f;

        while (elapsed_time <= cooltime)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float delta = elapsed_time / cooltime;
            m_skill_filed_image.fillAmount = 1f - delta;
        }

        m_skill_filed_image.fillAmount = 0f;
        m_skill_button.interactable = true;
    }

    public void BUTTON_Skill()
    {
        float default_duration = 10f;
        float final_duration = default_duration /* + 0.1 * 업그레이드 레벨 */;

        float default_spawn_interval = 1f;
        float final_spawn_interval = default_spawn_interval /* -0.25 * 업그레이드 레벨 */;

        UseSkill(final_duration, final_spawn_interval);
        CoolDownSkill();
    }

    private IEnumerator Co_SpawnMeteor(float target_time, float spawn_time)
    {
        float elapsed_time = 0f;
        float spawn_timer = 0f;

        while (elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            spawn_timer += Time.deltaTime;

            if (spawn_timer >= spawn_time)
            {
                float random_atk = Random.Range(m_skill_atk - 10f, m_skill_atk + 10f);

                var obj = ObjectManager.Instance.GetObject(ObjectType.METEOR);
                obj.transform.position = GetMeteorPosition();

                var meteor = obj.GetComponent<Meteor>();
                meteor.Initialize(random_atk, 7f);

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
    #endregion
}