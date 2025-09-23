using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntervalView : MonoBehaviour, IIntervalView
{
    [Header("UI 관련 컴포넌트")]
    [Header("생산 속도 강화 버튼")]
    [SerializeField] private Button m_upgrade_button;

    [Header("강화 버튼 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    private IntervalPresenter m_presenter;

    private void OnDestroy()
    {
        m_presenter.Dispose();
    }

    public void Inject(IntervalPresenter presenter)
    {
        m_presenter = presenter;

        m_upgrade_button.onClick.AddListener(m_presenter.OnClickedUpgrade);
    }

    public void UpdateUI(bool active, int upgrade_cost)
    {
        m_cost_label.text = active ? NumberFormatter.FormatNumber(upgrade_cost) :
                                     $"<color=red>{NumberFormatter.FormatNumber(upgrade_cost)}</color>";

        m_upgrade_button.interactable = active;
    }
}
