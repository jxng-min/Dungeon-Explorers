using InventoryService;
using UserDataService;

public class ResultPresenter
{
    #region Variables
    private readonly IResultViewer m_view;
    //private StageDataBase m_stage_db;
    private StageService m_stage_service;
    private IInventoryService m_inventory_service;
    private IUserDataService m_user_data_service;
    #endregion Variables

    //public ResultPresenter(IResultViewer view, StageDataBase stage_db, StageService stage_service, IInventoryService inventory_service, IUserDataService user_data_service)
    //{
    //    m_view = view;
    //    m_stage_db = stage_db;
    //   m_stage_service = stage_service;
    //    m_inventory_service = inventory_service;
    //    m_user_data_service = user_data_service;
    //}

    public void OpenView()
    {
        m_view.OpenView();

        var success = GameManager.Instance.GameState == GameEventType.GAMECLEAR;

        var stage = m_stage_service.GetStage(/*m_stage_db.Stage*/0);
        var final_money = success ? stage.Gold : stage.Gold / 4;
        var final_exp = success ? stage.EXP : stage.EXP / 4;

        UpdateModel(success, final_money, final_exp);

        m_view.UpdateUI(success, final_money, final_exp);
    }

    private void UpdateModel(bool success, int money, int exp)
    {
        m_inventory_service.Money += money;
        m_user_data_service.EXP += exp;

        //if (m_stage_db.Stage == m_user_data_service.Stage)
        //{
        //    m_user_data_service.Stage = success ? m_user_data_service.Stage + 1 : m_user_data_service.Stage;
        //}
    }

    public void OnClickedRetry()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void OnClickedTitle()
    {
        LoadingManager.Instance.LoadScene("Title");
    }
}
