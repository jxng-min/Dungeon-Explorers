public class InstantiatorPresenter
{
    #region Variables
    private readonly IInstantiatorView m_view;
    //private StageDataBase m_model;
    private ICostView m_cost_view;
    #endregion Variables

    #region Properties
    public ICostView CostView { get => m_cost_view; }
    #endregion Properties

    //public InstantiatorPresenter(IInstantiatorView view, StageDataBase model, ICostView cost_view)
    //{
    //    m_view = view;
    //    m_model = model;
    //    m_cost_view = cost_view;
    //}

    #region Helper Methods
    public void Initialize()
    {
    //    m_view.InitializeSlots(m_model.Deck);
    }
    #endregion Helper Methods
}
