public class IntervalModel
{
    private readonly int DEFAULT_UPGRADE_COST = 10;
    private readonly int GROWTH_UPGRADE_COST = 10;

    private int m_current_upgrade_level;

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
}
