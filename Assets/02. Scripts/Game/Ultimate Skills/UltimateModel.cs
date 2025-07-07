using ReinforcementService;

public class UltimateModel
{
    #region Variables
    private readonly int DEFAULT_ATK = 100;
    private readonly float DEAFULT_DURATION = 10f;
    private readonly float DEFAULT_INTERVAL = 1f;
    private readonly float DEFAULT_COOLTIME = 60f;

    private IReinforcementService m_reinforcement_system;
    #endregion Variables

    #region Properties
    public int ATK
    {
        get => DEFAULT_ATK
                + m_reinforcement_system.GetField(ReinforcementType.SKILL_DAMAGE - 1);
    }
    public float Duration { get => DEAFULT_DURATION; }
    public float Interval
    {
        get => DEFAULT_INTERVAL
                - m_reinforcement_system.GetField(ReinforcementType.SKILL_INTERVAL - 1) * 0.25f;
    }
    public float Cool
    {
        get => DEFAULT_COOLTIME
                - m_reinforcement_system.GetField(ReinforcementType.SKILL_COOLTIME - 1) * 0.25f;
    }
    #endregion Properties

    public UltimateModel(IReinforcementService reinforcement_system)
    {
        m_reinforcement_system = reinforcement_system;
    }
}
