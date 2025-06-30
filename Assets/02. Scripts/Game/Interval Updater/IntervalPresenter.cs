public class IntervalPresenter
{
    #region Variables
    private readonly IIntervalView m_view;
    private readonly IntervalModel m_model;
    #endregion Variables

    public IntervalPresenter(IIntervalView view, ICostView cost_view)
    {
        m_view = view;
        m_model = new IntervalModel(cost_view);
    }

    #region Helper Methods
    public int GetUpgrade()
    {
        return m_model.Upgrade;
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.UpgradeCost <= m_model.CurrentCost, m_model.UpgradeCost);
    }

    public void OnClickedUpgrade()
    {
        m_model.UpdateCost(m_model.UpgradeCost);
        m_model.Upgrade++;

        m_view.UpdateUI(m_model.UpgradeCost <= m_model.CurrentCost, m_model.UpgradeCost);
    }
    #endregion Helper Methods
}
