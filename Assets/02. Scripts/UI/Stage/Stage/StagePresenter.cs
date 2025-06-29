using UserDataService;

public class StagePresenter
{
    #region Variables
    private readonly IStageView m_view;
    private readonly StageModel m_model;
    #endregion Variables

    public StagePresenter(IStageView view, IUserDataService user_data_system, StageDataBase stage_db)
    {
        m_view = view;
        m_model = new StageModel(user_data_system, stage_db);
    }

    #region Helper Methods
    public void OnClickedPreviousButton()
    {
        m_model.Stage--;
        if (m_model.Stage < 1)
        {
            m_model.Stage = m_model.MaxStage;
        }

        m_view.UpdateUI(m_model.Stage, CheckState());
    }

    public void OnClickedNextButton()
    {
        m_model.Stage++;
        if (m_model.Stage > m_model.MaxStage)
        {
            m_model.Stage = 1;
        }

        m_view.UpdateUI(m_model.Stage, CheckState());
    }

    public void OnClickGameStart()
    {
        m_model.StageDataBase.Stage = m_model.Stage;
        
        LoadingManager.Instance.LoadScene("Game");
    }

    public void OnClickedOpenUI()
    {
        m_model.Initialize();

        m_view.OpenUI();
        m_view.UpdateUI(m_model.Stage, CheckState());
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }

    private StageState CheckState()
    {
        if (m_model.Stage < m_model.Record)
        {
            return StageState.CLEARED;
        }
        else if (m_model.Stage == m_model.Record)
        {
            return StageState.CHALLENGE;
        }
        else
        {
            return StageState.DENY;
        }
    }
    #endregion Helper Methods
}
