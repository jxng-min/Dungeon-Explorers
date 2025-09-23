using System;

public class IntervalPresenter : IDisposable
{
    private readonly IIntervalView m_view;
    private readonly IntervalModel m_model;

    private CostPresenter m_cost_presenter;

    public int Upgrade => m_model.Upgrade;

    public IntervalPresenter(IIntervalView view, 
                             IntervalModel model)
    {
        m_view = view;
        m_model = model;

        m_view.Inject(this);
    }

    public void Inject(CostPresenter cost_presenter)
    {
        m_cost_presenter = cost_presenter;

        m_cost_presenter.OnUpdatedCost += UpdateUI;
    }

    public void OnClickedUpgrade()
    {
        m_cost_presenter.UpdateCost(-m_model.UpgradeCost);
        m_model.Upgrade++;
    }

    public void UpdateUI(int cost)
    {
        var active = m_model.UpgradeCost <= cost;
        m_view.UpdateUI(active, m_model.UpgradeCost);
    }

    public void Dispose()
    {
        m_cost_presenter.OnUpdatedCost -= UpdateUI;
    }
}