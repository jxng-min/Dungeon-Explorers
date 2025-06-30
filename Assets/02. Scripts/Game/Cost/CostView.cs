using System.Collections;
using ReinforcementService;
using TMPro;
using UnityEngine;

public class CostView : MonoBehaviour, ICostView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [SerializeField] private TMP_Text m_cost_label;

    private CostPresenter m_presenter;
    private IReinforcementService m_reinforcement_system;
    #endregion Variables

    private void Awake()
    {
        m_reinforcement_system = ServiceLocator.Instance.ReinforceService;

        m_presenter = new CostPresenter(this, m_reinforcement_system);
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Subscribe(GameEventType.GAMEOVER, GameManager.Instance.GameOver);
        GameEventBus.Subscribe(GameEventType.GAMECLEAR, GameManager.Instance.GameClear);
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

    #region Helper Methods
    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(float current_cost, float max_cost)
    {
        m_cost_label.text = $"{NumberFormatter.FormatNumber(current_cost)}/{NumberFormatter.FormatNumber(max_cost)}";
    }

    public void UpdateCost(int cost)
    {
        m_presenter.UpdateCost(cost);
    }

    public void StartUI(float interval)
    {
        StartCoroutine(Co_UpdateCost(interval));
    }

    private IEnumerator Co_UpdateCost(float interval)
    {
        float elapsed_time = 0f;

        while (true)
        {
            elapsed_time += Time.deltaTime;

            if (elapsed_time >= interval)
            {
                m_presenter.UpdateCost(1);
                elapsed_time = 0f;
            }

            yield return null;
        }
    }
    #endregion Helper Methods
}
