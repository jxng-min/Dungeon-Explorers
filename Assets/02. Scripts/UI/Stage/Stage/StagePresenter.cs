using UserService;

public class StagePresenter
{
    private readonly IStageView m_view;
    private readonly IStageDataBase m_stage_db;
    private readonly IUserService m_user_service;

    private int m_stage;

    public StagePresenter(IStageView view,
                          IStageDataBase stage_db,
                          IUserService user_service)
    {
        m_view = view;
        m_stage_db = stage_db;
        m_user_service = user_service;

        m_user_service.OnUpdatedStage += UpdateUI;

        m_view.Inject(this);
        OpenUI();
    }

    public void OpenUI()
    {
        m_view.OpenUI();
        UpdateUI(m_user_service.Stage);

        m_view.PlaySFX("Button Click");
    }

    public void UpdateUI(int stage)
    {
        m_stage = stage;
        string state_text;

        if(m_user_service.Stage > stage)
        {
            state_text = "<color=green>완료</color>";
        }
        else if(m_user_service.Stage == stage)
        {
            state_text = "<color=yellow>진행 중</color>";
        }
        else
        {
            state_text = "<color=red>잠김</color>";
        }

        m_view.UpdateUI(stage, state_text);
    }

    public void CloseUI()
    {
        m_view.CloseUI();
    }

    public void OnClickLeft()
    {
        var prev_stage = ((m_stage - 2 + m_stage_db.Count) % m_stage_db.Count) + 1;
        UpdateUI(prev_stage);

        m_view.PlaySFX("Button Click");
    }

    public void OnClickRight()
    {
        var next_stage = (m_stage % m_stage_db.Count) + 1;
        UpdateUI(next_stage);

        m_view.PlaySFX("Button Click");
    }

    public void OnClickStart()
    {
        m_stage_db.Current = m_stage;
        LoadingManager.Instance.LoadScene("Game");

        m_view.PlaySFX("Button Click");
    }
}