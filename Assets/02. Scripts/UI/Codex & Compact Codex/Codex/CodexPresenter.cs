using UnitService;

public class CodexPresenter
{
    private readonly ICodexView m_view;
    private readonly IUnitDataBase m_unit_db;
    private readonly CompactCodexPresenter m_compact_presenter;

    public CodexPresenter(ICodexView view, 
                          IUnitDataBase unit_db,
                          CompactCodexPresenter compact_presenter)
    {
        m_view = view;
        m_unit_db = unit_db;
        m_compact_presenter = compact_presenter;

        m_view.Inject(this);
    }

    public void Initialize()
    {
        for(int i = 0; i < m_unit_db.GreenList.Count; i++)
        {
            var codex_slot_view = m_view.InstantiateSlot();

            var codex_slot_presenter = new CodexSlotPresenter(codex_slot_view,
                                                              m_compact_presenter,
                                                              m_unit_db.GreenList[i]);
        }
    }

    public void OpenUI()
    {
        m_view.OpenUI();
    }

    public void CloseUI()
    {
        m_view.CloseUI();
        m_compact_presenter.CloseUI();
    }
}
