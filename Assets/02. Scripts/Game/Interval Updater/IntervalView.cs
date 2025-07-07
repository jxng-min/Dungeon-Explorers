using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntervalView : MonoBehaviour, IIntervalView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("코스트 업데이터")]
    [SerializeField] private CostView m_cost_view;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [Header("생산 속도 강화 버튼")]
    [SerializeField] private Button m_upgrade_button;

    [Header("강화 버튼 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    private IntervalPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_presenter = new IntervalPresenter(this, m_cost_view);

        m_upgrade_button.onClick.AddListener(m_presenter.OnClickedUpgrade);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameEventType.PLAYING)
        {
            m_presenter.UpdateView();
        }
    }

    #region Helper Methods
    public int GetUpgrade()
    {
        return m_presenter.GetUpgrade();
    }

    public void UpdateUI(bool active, int upgrade_cost)
    {
        if (active)
        {
            m_cost_label.text = NumberFormatter.FormatNumber(upgrade_cost);
        }
        else
        {
            m_cost_label.text = $"<color=red>{NumberFormatter.FormatNumber(upgrade_cost)}</color>";
        }

        m_upgrade_button.interactable = active;
    }
    #endregion Helper Methods
}
