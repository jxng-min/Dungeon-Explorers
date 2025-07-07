using DeckService;

public class DeckPresenter
{
    #region Variables
    private readonly IDeckView m_view;
    private readonly IDeckService m_model;
    private StageDataBase m_stage_db;
    #endregion Variables

    public DeckPresenter(IDeckView view, IDeckService model, StageDataBase stage_db)
    {
        m_view = view;
        m_model = model;
        m_stage_db = stage_db;
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
        m_stage_db.Deck = m_model.GetDeck().ToArray();
        m_view.CloseUI();
    }
}