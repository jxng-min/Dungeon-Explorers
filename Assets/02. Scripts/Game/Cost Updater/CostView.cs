using System.Collections;
using TMPro;
using UnityEngine;

public class CostView : MonoBehaviour, ICostView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [SerializeField] private TMP_Text m_cost_label;

    private CostPresenter m_presenter;

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

    public void Inject(CostPresenter presenter)
    {
        m_presenter = presenter;
    }

    public void StartUI()
    {
        StartCoroutine(Co_UpdateCost());
    }

    public void UpdateUI(float current_cost, float max_cost)
    {
        m_cost_label.text = $"{current_cost.ToString("0000")}/{max_cost.ToString("0000")}";
    }

    private IEnumerator Co_UpdateCost()
    {
        float elapsed_time = 0f;

        while (true)
        {
            elapsed_time += Time.deltaTime;

            if (elapsed_time >= m_presenter.Interval)
            {
                m_presenter.UpdateCost(1);
                elapsed_time = 0f;
            }

            yield return null;
        }
    }
    #endregion Helper Methods
}
