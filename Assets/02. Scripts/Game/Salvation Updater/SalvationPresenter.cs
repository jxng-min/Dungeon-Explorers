public class SalvationPresenter
{
    private readonly ISalvationView m_view;
    private readonly SalvationModel m_model;

    public SalvationPresenter(ISalvationView view, 
                              SalvationModel model)
    {
        m_view = view;
        m_model = model;

        m_view.Inject(this);
    }

    #region Helper Methods
    public void Use()
    {
        m_view.UseUI(m_model.Duration, m_model.Interval, m_model.ATK);
        m_view.CoolUI(m_model.Cool);
    }
    #endregion Helper Methods
}
