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
    private IIntervalView m_interval_view;
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
        get => DEFAULT_INTERVAL
                - GROWTH_INTERVAL * m_interval_view.GetUpgrade();
    }
    #endregion Properties

    public CostModel(IReinforcementService reinforcement_system, IIntervalView interval_view)
    {
        m_reinforcement_system = reinforcement_system;
        m_interval_view = interval_view;
    }
}
