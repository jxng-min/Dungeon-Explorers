using ReinforcerService;
using UnityEngine;

public class CostPresenter
{
    #region Variables
    private readonly ICostView m_view;
    private readonly CostModel m_model;
    #endregion Variables

    public CostPresenter(ICostView view, IReinforcerService reinforcement_system, IIntervalView interval_view)
    {
        m_view = view;
        m_model = new CostModel(reinforcement_system, interval_view);
    }

    #region Helper Methods
    public void Initialize()
    {
        m_view.StartUI();
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

    public int GetCost()
    {
        return m_model.Cost;
    }

    public float GetInterval()
    {
        return m_model.Interval;
    }
    #endregion Helper Methods
}
