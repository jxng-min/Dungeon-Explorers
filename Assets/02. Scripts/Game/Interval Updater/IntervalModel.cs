public class IntervalModel
{
    #region Variables
    private const int DEFAULT_UPGRADE_COST = 10;
    private const int GROWTH_UPGRADE_COST = 10;

    private int m_current_upgrade_level;

    private ICostView m_cost_view;
    #endregion Variables

    #region Properties
    public int Upgrade
    {
        get => m_current_upgrade_level;
        set => m_current_upgrade_level = value;
    }

    public int UpgradeCost
    {
        get => DEFAULT_UPGRADE_COST
                + GROWTH_UPGRADE_COST * m_current_upgrade_level;
    }

    public int CurrentCost { get => m_cost_view.GetCost(); }
    #endregion Properties

    public IntervalModel(ICostView view)
    {
        m_cost_view = view;
        m_current_upgrade_level = 0;
    }

    public void UpdateCost(int cost)
    {
        m_cost_view.UpdateCost(-cost);
    }
}
