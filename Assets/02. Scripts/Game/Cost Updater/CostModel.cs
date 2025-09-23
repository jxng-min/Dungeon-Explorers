using ReinforcerService;

public class CostModel
{
    private readonly IReinforcerService m_reinforcer_service;
    private readonly IntervalPresenter m_interval_presenter;

    private readonly float DEFAULT_INTERVAL = 0.2f;
    private readonly float GROWTH_INTERVAL = 0.005f;

    private readonly int DEFAULT_MAX_COST = 100;
    private readonly int GROWTH_COST = 10;

    private int m_current_cost;

    public int Cost
    {
        get => m_current_cost;
        set => m_current_cost = value;
    }

    public int MaxCost
    {
        get => DEFAULT_MAX_COST
                + GROWTH_COST * m_reinforcer_service.GetField(ReinforcementType.INCREASE_MAX_COST);
    }

    public float Interval
    {
        get => DEFAULT_INTERVAL
                - GROWTH_INTERVAL * m_interval_presenter.Upgrade;
    }

    public CostModel(IReinforcerService reinforcer_service, 
                     IntervalPresenter interval_presenter)
    {
        m_reinforcer_service = reinforcer_service;
        m_interval_presenter = interval_presenter;
    }
}
