using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserDataService;

[RequireComponent(typeof(Animator))]
public class StageView : MonoBehaviour, IStageView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] StageDataBase m_stage_db;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [Header("스테이지 표기 라벨")]
    [SerializeField] private TMP_Text m_stage_label;

    [Header("스테이지 상태 라벨")]
    [SerializeField] private TMP_Text m_status_label;

    [Header("이전 스테이지 버튼")]
    [SerializeField] private Button m_previous_button;

    [Header("다음 스테이지 버튼")]
    [SerializeField] private Button m_next_button;

    [Header("게임 시작 버튼")]
    [SerializeField] private Button m_start_button;

    [Header("UI 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private StagePresenter m_presenter;
    private IUserDataService m_inventory_system;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_inventory_system = ServiceLocator.Get<IUserDataService>();

        m_presenter = new StagePresenter(this, m_inventory_system, m_stage_db);

        m_previous_button.onClick.AddListener(m_presenter.OnClickedPreviousButton);
        m_next_button.onClick.AddListener(m_presenter.OnClickedNextButton);

        m_start_button.onClick.AddListener(m_presenter.OnClickGameStart);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    #region Helper Methods
    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void UpdateUI(int stage, StageState stage_status)
    {
        m_stage_label.text = $"지하 던전 {stage}층";

        m_start_button.interactable = true;
        switch (stage_status)
        {
            case StageState.CLEARED:
                m_status_label.text = "<color=green>토벌 완료</color>";
                break;

            case StageState.CHALLENGE:
                m_status_label.text = "<color=yellow>토벌 가능</color>";
                break;

            case StageState.DENY:
                m_status_label.text = "<color=red>토벌 불가</color>";
                m_start_button.interactable = false;
                break;
        }
    }
    #endregion Helper Methods
}
