using System.Collections;
using ReinforcementService;
using TMPro;
using UnityEngine;

public class CostView : MonoBehaviour, ICostView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("인터벌 업데이터")]
    [SerializeField] private IntervalView m_interval_view;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [SerializeField] private TMP_Text m_cost_label;

    private CostPresenter m_presenter;
    private IReinforcementService m_reinforcement_system;
    #endregion Variables

    private void Awake()
    {
        m_reinforcement_system = ServiceLocator.Instance.ReinforceService;

        m_presenter = new CostPresenter(this, m_reinforcement_system, m_interval_view);
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Subscribe(GameEventType.GAMEOVER, GameManager.Instance.GameOver);
        GameEventBus.Subscribe(GameEventType.GAMECLEAR, GameManager.Instance.GameClear);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Unsubscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Unsubscribe(GameEventType.GAMEOVER, GameManager.Instance.GameOver);
        GameEventBus.Unsubscribe(GameEventType.GAMECLEAR, GameManager.Instance.GameClear);
    }

    private void Start()
    {
        m_presenter.Initialize();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameEventType.PLAYING)
        {
            m_presenter.UpdateView();
        }
    }

    #region Helper Methods
    public void UpdateUI(float current_cost, float max_cost)
    {
        m_cost_label.text = $"{NumberFormatter.FormatNumber(current_cost)}/{NumberFormatter.FormatNumber(max_cost)}";
    }

    public void UpdateCost(int cost)
    {
        m_presenter.UpdateCost(cost);
    }

    public void StartUI()
    {
        StartCoroutine(Co_UpdateCost());
    }

    private IEnumerator Co_UpdateCost()
    {
        float elapsed_time = 0f;

        while (true)
        {
            elapsed_time += Time.deltaTime;

            if (elapsed_time >= m_presenter.GetInterval())
            {
                m_presenter.UpdateCost(1);
                elapsed_time = 0f;
            }

            yield return null;
        }
    }

    public int GetCost()
    {
        return m_presenter.GetCost();
    }
    #endregion Helper Methods
}
