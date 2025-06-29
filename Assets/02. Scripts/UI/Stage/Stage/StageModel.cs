using UserDataService;

public class StageModel
{
    #region Variables
    private IUserDataService m_user_data_system;
    private StageDataBase m_stage_db;
    private int m_stage_pointer;
    private const int MAX_STAGE = 100;
    #endregion Variables

    #region Properties
    public int MaxStage { get => MAX_STAGE; } 

    public int Stage
    {
        get => m_stage_pointer;
        set => m_stage_pointer = value;
    }

    public int Record
    {
        get => m_user_data_system.Stage;
    }

    public StageDataBase StageDataBase { get => m_stage_db; }
    #endregion Properties

    public StageModel(IUserDataService user_data_system, StageDataBase stage_db)
    {
        m_user_data_system = user_data_system;
        m_stage_db = stage_db;
    }

    #region Helper Methods 
    public void Initialize()
    {
        m_stage_pointer = m_user_data_system.Stage;
    }
    #endregion Helper Methods
}
