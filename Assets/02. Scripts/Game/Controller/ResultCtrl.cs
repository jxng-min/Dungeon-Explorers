using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ResultCtrl : MonoBehaviour
{
    [Header("몬스터 팩토리")]
    [SerializeField] private EnemyFactory m_factory;

    [Header("게임 결과 라벨")]
    [SerializeField] private TMP_Text m_result_label;

    [Header("골드 보상 라벨")]
    [SerializeField] private TMP_Text m_gold_label;

    [Header("경험치 보상 라벨")]
    [SerializeField] private TMP_Text m_exp_label;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Open()
    {
        m_animator.SetBool("Open", true);
        Initialize();
    }

    private void Initialize()
    {
        if (GameManager.Instance.GameState == GameEventType.GAMECLEAR)
        {
            m_result_label.text = $"토벌 결과: <color=green>성공</color>";
            m_gold_label.text = $"골드 보상: {NumberFormatter.FormatNumber(m_factory.Stage.Gold)}";
            m_exp_label.text = $"경험치 보상: {NumberFormatter.FormatNumber(m_factory.Stage.EXP)}";
        }
        else
        {
            m_result_label.text = $"토벌 결과: <color=red>실패</color>";
            m_gold_label.text = $"골드 보상: {NumberFormatter.FormatNumber(m_factory.Stage.Gold / 4)}";
            m_exp_label.text = $"경험치 보상: {NumberFormatter.FormatNumber(m_factory.Stage.EXP / 4)}";
        }
    }

    public void BUTTON_Retry()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void BUTTON_Title()
    {
        LoadingManager.Instance.LoadScene("Title");
    }
}
