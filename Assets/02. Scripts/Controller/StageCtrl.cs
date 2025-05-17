using System.Collections;
using TMPro;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("빌드 컨트롤러")]
    [SerializeField] private BuildCtrl m_build_ctrl;

    [Space(30)]
    [Header("생산량 상태 라벨")]
    [SerializeField] private TMP_Text m_cost_state_label;
    #endregion

    public const float INTERVAL_UPGRADE = 0.01f;

    private int m_default_max_cost = 100;
    private float m_default_cost_interval = 0.2f;

    private int m_current_max_cost;
    private int m_current_cost;
    public int Cost
    {
        get => m_current_cost;
        set => m_current_cost = value;
    }
    private float m_current_cost_interval;
    public float Interval
    {
        get => m_current_cost_interval;
        set => m_current_cost_interval = value;
    }

    private void Start()
    {
        InitializeCost();

        StartCoroutine(Co_CostIncrease());
    }

    private void Update()
    {
        // TODO: PLAYING이 아니면 업데이트 안함.
        UpdateUI();   
    }

    private void UpdateUI()
    {
        m_cost_state_label.text = $"{m_current_cost}/{m_current_max_cost}";
    }

    private void InitializeCost()
    {
        m_current_max_cost = m_default_max_cost /* + 10 * 강화 레벨 */;
        m_current_cost = 0;
        m_current_cost_interval = m_default_cost_interval;
    }

    public void UpdateCurrentCost(int amount)
    {
        m_current_cost += amount;
        if (m_current_cost < 0)
        {
            m_current_cost = 0;
        }
        
        if(m_current_cost > m_current_max_cost)
        {
            m_current_cost = m_current_max_cost;
        }
    }

    private IEnumerator Co_CostIncrease()
    {
        float elapsed_time = 0f;

        while (true)
        {
            elapsed_time += Time.deltaTime;

            if (elapsed_time >= m_current_cost_interval)
            {
                UpdateCurrentCost(1);
                elapsed_time = 0f;
            }

            yield return null;
        }
    }
}