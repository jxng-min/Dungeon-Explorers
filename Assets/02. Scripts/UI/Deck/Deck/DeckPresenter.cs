using DeckService;

public class DeckPresenter
{
    #region Variables
    private readonly IDeckView m_view;
    private readonly IDeckService m_model;
    #endregion Variables

    public DeckPresenter(IDeckView view, IDeckService model)
    {
        m_view = view;
        m_model = model;
    }

    public void Initialize()
    {
        m_view.Initialize();
    }

    public void OnClickedOpenUI()
    {
        m_view.OpenUI();
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }
}
