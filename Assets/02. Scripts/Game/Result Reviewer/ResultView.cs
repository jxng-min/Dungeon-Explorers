using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ResultView : MonoBehaviour, IResultView
{
    [Header("UI 관련 컴포넌트")]
    [Header("게임 결과")]
    [SerializeField] private TMP_Text m_result_label;

    [Header("골드 보상")]
    [SerializeField] private TMP_Text m_gold_label;

    [Header("경험치 보상")]
    [SerializeField] private TMP_Text m_exp_label;

    [Header("리플레이 버튼")]
    [SerializeField] private Button m_retry_button;

    [Header("타이틀 버튼")]
    [SerializeField] private Button m_title_button;

    private Animator m_animator;

    private ResultPresenter m_presenter;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        m_retry_button.onClick.RemoveListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.RemoveListener(m_presenter.OnClickedTitle);

        m_presenter?.Dispose();
    }

    public void Inject(ResultPresenter presenter)
    {
        m_presenter = presenter;

        m_retry_button.onClick.AddListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.AddListener(m_presenter.OnClickedTitle);
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
    }

    public void UpdateUI(bool success, int money, int exp)
    {
        m_result_label.text = success ? "<color=green>성공</color>" : "<color=red>실패</color>";
        m_gold_label.text = NumberFormatter.FormatNumber(money);
        m_exp_label.text = NumberFormatter.FormatNumber(exp);
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }
}
