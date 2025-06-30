using ReinforcementService;

public class CostModel
{
    #region Variables
    private const float DEFAULT_INTERVAL = 0.2f;
    private const float GROWTH_INTERVAL = 0.01f;

    private const int DEFAULT_MAX_COST = 100;
    private const int GROWTH_COST = 10;

    private int m_current_cost;

    private IReinforcementService m_reinforcement_system;
    #endregion Variables

    #region Properties
    public int Cost
    {
        get => m_current_cost;
        set => m_current_cost = value;
    }

    public int MaxCost
    {
        get => DEFAULT_MAX_COST
                + GROWTH_COST * m_reinforcement_system.GetField(ReinforcementType.INCREASE_MAX_COST);
    }

    public float Interval
    {
        // TODO: 타워 레벨에 따라 수정 필요
        get => DEFAULT_INTERVAL
                + GROWTH_INTERVAL + 1;
    }
    #endregion Properties

    public CostModel(IReinforcementService reinforcement_system)
    {
        m_reinforcement_system = reinforcement_system;
    }
}
