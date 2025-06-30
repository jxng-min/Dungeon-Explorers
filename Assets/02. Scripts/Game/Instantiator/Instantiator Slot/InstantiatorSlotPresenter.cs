using Units;

public class InstantiatorSlotPresenter
{
    #region Variables
    private readonly IInstantiatorSlotView m_view;
    private readonly InstantiatorSlotModel m_model;
    #endregion Variables

    public InstantiatorSlotPresenter(IInstantiatorSlotView view)
    {
        m_view = view;
        m_model = new InstantiatorSlotModel();
    }

    public void Initialize(UnitCode code, UnitDataBase unit_db, ICostView cost_view)
    {
        m_model.Initialize(code, unit_db, cost_view);

        m_view.ClearUI();

        if (m_model.Code != UnitCode.EMPTY)
        {
            m_view.InitUI(m_model.Image, m_model.UnitCost);
        }
    }

    public void UpdateView()
    {
        if (m_model.Code != UnitCode.EMPTY)
        {
            m_view.ToggleUI(m_model.UnitCost <= m_model.CurrentCost, m_model.UnitCost);
        }
    }

    public void OnClickedInstantiation()
    {
        m_view.CoolUI(m_model.Cool);

        // 유닛 소환
        m_model.UpdateCost(-m_model.UnitCost);
    }
}
