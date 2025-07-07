using InventoryService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserDataService;

[RequireComponent(typeof(Animator))]
public class ResultViewer : MonoBehaviour, IResultViewer
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    [Header("스테이지 서비스")]
    [SerializeField] private StageService m_stage_system;

    [Space(50f)]
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
    private IInventoryService m_inventory_service;
    private IUserDataService m_user_data_system;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_inventory_service = ServiceLocator.Get<IInventoryService>();
        m_user_data_system = ServiceLocator.Get<IUserDataService>();

        m_presenter = new ResultPresenter(this, m_stage_db, m_stage_system, m_inventory_service, m_user_data_system);

        m_retry_button.onClick.AddListener(m_presenter.OnClickedRetry);
        m_title_button.onClick.AddListener(m_presenter.OnClickedTitle);
    }

    public void OpenUI()
    {
        m_presenter.OpenView();
    }

    public void OpenView()
    {
        m_animator.SetBool("Open", true);
    }

    public void UpdateUI(bool success, int money, int exp)
    {
        m_result_label.text = "토벌 결과: " + (success ? "<color=green>성공</color>" : "<color=red>실패</color>");
        m_gold_label.text = $"골드 보상: {NumberFormatter.FormatNumber(money)}";
        m_exp_label.text = $"EXP 보상: {NumberFormatter.FormatNumber(exp)}";
    }
}
