using ReinforcementService;

public class UltimatePresenter
{
    #region Variables
    private readonly IUltimateView m_view;
    private readonly UltimateModel m_model;
    #endregion Variables

    public UltimatePresenter(IUltimateView view, IReinforcementService reinforcement_system)
    {
        m_view = view;
        m_model = new UltimateModel(reinforcement_system);
    }

    #region Helper Methods
    public void OnClickedUseButton()
    {
        m_view.UseUI(m_model.Duration, m_model.Interval, m_model.ATK);
        m_view.CoolUI(m_model.Cool);
    }
    #endregion Helper Methods
}
