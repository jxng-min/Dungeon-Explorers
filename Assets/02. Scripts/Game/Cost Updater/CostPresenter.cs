using System;

public class CostPresenter
{
    private readonly ICostView m_view;
    private readonly CostModel m_model;

    public event Action<int> OnUpdatedCost;

    public float Interval => m_model.Interval;

    public CostPresenter(ICostView view,
                         CostModel model)
    {
        m_view = view;
        m_model = model;

        m_view.Inject(this);
        m_view.StartUI();
    }

    public void UpdateCost(int cost)
    {
        m_model.Cost += cost;
        m_model.Cost = UnityEngine.Mathf.Clamp(m_model.Cost, 0, m_model.MaxCost);

        OnUpdatedCost?.Invoke(m_model.Cost);
        m_view.UpdateUI(m_model.Cost, m_model.MaxCost);
    }

    public void UpdateView()
    {
        m_view.UpdateUI(m_model.Cost, m_model.MaxCost);
    }
}
