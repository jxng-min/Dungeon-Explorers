using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StageSelector : MonoBehaviour
{
    const int MAX_STAGE = 100;

    [Header("현재 층 수를 나타내는 라벨")]
    [SerializeField] private TMP_Text m_floor_label;

    [Header("현재 토벌 상태를 나타내는 라벨")]
    [SerializeField] private TMP_Text m_clear_state_label;

    private Animator m_stage_animator;

    private int m_current_stage = 0;

    private void Awake()
    {
        m_stage_animator = GetComponent<Animator>();
    }

    private void Initialize()
    {
        m_current_stage = DataManager.Instance.Data.Stage;
        SetStageInfo();
    }

    private void SetStageInfo()
    {
        m_floor_label.text = $"지하 던전 {m_current_stage}층";

        int mark_stage = DataManager.Instance.Data.Stage;
        if (m_current_stage < mark_stage)
        {
            m_clear_state_label.text = $"<color=green>토벌 완료</color>";
        }
        else if (m_current_stage == mark_stage)
        {
            m_clear_state_label.text = $"<color=yellow>토벌 미완료</color>";
        }
        else
        {
            m_clear_state_label.text = "<color=red>토벌 불가능</color>";
        }
    }

    public void BUTTON_Left()
    {
        m_current_stage--;
        if (m_current_stage < 1)
        {
            m_current_stage = MAX_STAGE;
        }

        SetStageInfo();
    }

    public void BUTTON_Right()
    {
        m_current_stage++;
        if (m_current_stage > MAX_STAGE)
        {
            m_current_stage = 1;
        }

        SetStageInfo();
    }

    public void BUTTON_Open()
    {
        m_stage_animator.SetBool("Open", true);
        Initialize();
    }

    public void BUTTON_Close()
    {
        m_stage_animator.SetBool("Open", false);
    }

    public void BUTTON_Start()
    {
        StageManager.Instance.SetParty(DataManager.Instance.Data.Party);
        LoadingManager.Instance.LoadScene("Game");
    }
}
