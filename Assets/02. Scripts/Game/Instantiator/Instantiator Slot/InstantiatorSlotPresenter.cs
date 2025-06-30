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

    public void Initialize(UnitCode code, UnitDataBase unit_db)
    {
        m_model.Initialize(code, unit_db);

        m_view.ClearUI();

        if (m_model.Code != UnitCode.EMPTY)
        {
            m_view.InitUI(m_model.Image, m_model.Cost);
        }
    }

    public void UpdateView()
    {
        // m_view.ToggleUI(m_model.Cost, m_model.Cost);
    }

    public void OnClickedInstantiation()
    {
        m_view.CoolUI(m_model.Cool);

        // 유닛 소환
        // 코스트 비용 갱신
    }
}
