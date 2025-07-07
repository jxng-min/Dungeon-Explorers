using Units;

public class CodexInfoPresenter
{
    #region Variables
    private ICodexInfoView m_view;
    private UnitDataBase m_model;
    private IUnitRepository m_unit_repo;
    #endregion Variables

    public CodexInfoPresenter(ICodexInfoView view, UnitDataBase model, IUnitRepository unit_repo)
    {
        m_view = view;
        m_model = model;
        m_unit_repo = unit_repo;
    }

    #region Helper Methods
    public void OnClickedOpenUI(UnitCode code)
    {
        var unit = m_model.GetUnit(code);

        var image = unit.Image;
        var name = m_unit_repo.GetName(code);
        var description = m_unit_repo.GetDescription(code);
        m_view.OpenUI(image, name, description);
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }
    #endregion Helper Methods
}
