using ReinforcerService;

public class SalvationModel
{
    private readonly int DEFAULT_ATK = 100;
    private readonly float DEAFULT_DURATION = 10f;
    private readonly float DEFAULT_INTERVAL = 1f;
    private readonly float DEFAULT_COOLTIME = 60f;

    private IReinforcerService m_reinforcer_service;

    public int ATK          => DEFAULT_ATK
                                    + m_reinforcer_service.GetField(ReinforcementType.SKILL_DAMAGE - 1);
    public float Duration   => DEAFULT_DURATION;
    public float Interval   => DEFAULT_INTERVAL
                                    - m_reinforcer_service.GetField(ReinforcementType.SKILL_INTERVAL - 1) * 0.01f;
    public float Cool       => DEFAULT_COOLTIME
                                    - m_reinforcer_service.GetField(ReinforcementType.SKILL_COOLTIME - 1) * 0.1f;

    public SalvationModel(IReinforcerService reinforcer_service)
    {
        m_reinforcer_service = reinforcer_service;
    }
}
