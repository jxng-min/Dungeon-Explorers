using UnitService;

public class CompactCodexPresenter
{
    private readonly ICompactCodexView m_view;
    private readonly IUnitService m_unit_service;

    public CompactCodexPresenter(ICompactCodexView view,
                                 IUnitService unit_service)
    {
        m_view = view;
        m_unit_service = unit_service;

        m_view.Inject(this);
    }

    public void OpenUI(Unit unit)
    {
        m_view.OpenUI();
        m_view.UpdateUI(unit.Image,
                        m_unit_service.GetName(unit.Code),
                        m_unit_service.GetDescription(unit.Code));
    }

    public void CloseUI()
    {
        m_view.CloseUI();
    }
}
