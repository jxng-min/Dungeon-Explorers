public class InstantiatorPresenter
{
    #region Variables
    private readonly IInstantiatorView m_view;
    private StageDataBase m_model;
    #endregion Variables

    public InstantiatorPresenter(IInstantiatorView view, StageDataBase model)
    {
        m_view = view;
        m_model = model;
    }

    #region Helper Methods
    public void Initialize()
    {
        m_view.InitializeSlots(m_model.Deck);
    }
    #endregion Helper Methods
}
