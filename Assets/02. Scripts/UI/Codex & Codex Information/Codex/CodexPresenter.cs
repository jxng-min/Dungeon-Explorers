using Units;

public class CodexPresenter
{
    #region Variables
    private readonly ICodexView m_view;
    private readonly UnitDataBase m_unit_db;
    #endregion Variables

    public CodexPresenter(ICodexView view, UnitDataBase unit_db)
    {
        m_view = view;
        m_unit_db = unit_db;
    }

    public void Initialize()
    {
        for (int i = 0; i < m_unit_db.Count; i++)
        {
            var unit = m_unit_db.GetUnit((UnitCode)i);
            m_view.Initialize(unit);
        }
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
