using ReinforcementService;

public class ReinforcementPresenter
{
    #region Variables
    private readonly IReinforcementView m_view;
    private readonly IReinforcementService m_model;
    #endregion Variables

    public ReinforcementPresenter(IReinforcementView view, IReinforcementService model)
    {
        m_view = view;
        m_model = model;
    }

    #region Helper Methods
    public void Initialize()
    {
        m_view.Initialize(m_model.GetDict());
    }

    public void OnClickedOpenUI()
    {
        m_view.OpenUI();
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }
    #endregion Helper Methods
}
