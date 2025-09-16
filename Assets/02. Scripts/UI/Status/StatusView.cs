using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : MonoBehaviour, IStatusView
{
    [Header("UI 관련 컴포넌트")]
    [Header("레벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("경험치")]
    [SerializeField] private Slider m_exp_slider;

    [Header("돈")]
    [SerializeField] private TMP_Text m_money_label; 

    private StatusPresenter m_presenter;

    private void OnDestroy()
    {
        m_presenter.Dispose();
    }

    public void Inject(StatusPresenter presenter)
    {
        m_presenter = presenter;
    }

    public void UpdateLevel(int level, float exp_rate)
    {
        m_level_label.text = $"LV.{NumberFormatter.FormatNumber(level)}";
        m_exp_slider.value = exp_rate;
    }

    public void UpdateMoney(int money)
    {
        m_money_label.text = NumberFormatter.FormatNumber(money);
    }
}
