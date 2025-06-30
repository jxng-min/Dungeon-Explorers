using ReinforcementService;
using UnityEngine;

public class CostPresenter
{
    #region Variables
    private readonly ICostView m_view;
    private readonly CostModel m_model;
    #endregion Variables

    public CostPresenter(ICostView view, IReinforcementService reinforcement_system)
    {
        m_view = view;
        m_model = new CostModel(reinforcement_system);
    }

    #region Helper Methods
    public void Initialize()
    {
        m_view.StartUI(m_model.Interval);
    }
    public void UpdateCost(int cost)
    {
        m_model.Cost += cost;
        m_model.Cost = Mathf.Clamp(m_model.Cost, 0, m_model.MaxCost);
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.Cost, m_model.MaxCost);
    }
    #endregion Helper Methods
}
