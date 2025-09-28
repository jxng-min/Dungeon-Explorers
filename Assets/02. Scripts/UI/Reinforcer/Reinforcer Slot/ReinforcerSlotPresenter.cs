using System;
using System.Collections.Generic;
using InventoryService;
using ReinforcerService;

public class ReinforcerSlotPresenter : IDisposable
{
    private readonly IReinforcerSlotView m_view;

    private readonly IInventoryService m_inventory_service;
    private readonly IReinforcerService m_reinforcer_service;

    private readonly ReinforcementItem m_reinforcement_item;

    public ReinforcerSlotPresenter(IReinforcerSlotView view,
                               IInventoryService inventory_service,
                               IReinforcerService reinforcer_service,
                               ReinforcementItem reinforcement_item)
    {
        m_view = view;

        m_inventory_service = inventory_service;
        m_reinforcer_service = reinforcer_service;

        m_reinforcement_item = reinforcement_item;

        m_inventory_service.OnUpdatedMoney += UpdateMoney;
        m_reinforcer_service.OnUpdatedReinforcement += UpdateLimit;

        m_view.Inject(this);
        Initialize();
    }

    public void Initialize()
    {
        m_view.UpdateUI(m_reinforcement_item.Name, m_reinforcement_item.Image);

        UpdateMoney(m_inventory_service.Money);
        UpdateLimit(m_reinforcement_item.Type, m_reinforcer_service.GetField(m_reinforcement_item.Type));
    }

    public void UpdateMoney(int money)
    {
        var total_cost = m_reinforcement_item.DefaultCost
                            + m_reinforcement_item.GrowthCost * (m_reinforcer_service.GetField(m_reinforcement_item.Type) - 1);

        var can_purchase = total_cost <= money;

        m_view.UpdateCost(total_cost, can_purchase);
    }

    public void UpdateLimit(ReinforcementType type, int level)
    {
        if(type != m_reinforcement_item.Type)
        {
            return;
        }

        var is_limit = level >= m_reinforcement_item.Limit;
        m_view.UpdateReinforcement(level, is_limit);
    }

    public void OnClickedReinforcement()
    {
        var total_cost = m_reinforcement_item.DefaultCost
                            + m_reinforcement_item.GrowthCost * (m_reinforcer_service.GetField(m_reinforcement_item.Type) - 1);
        
        m_inventory_service.UpdateMoney(-total_cost);
        m_reinforcer_service.UpgradeField(m_reinforcement_item.Type);

        m_view.PlaySFX("Button Click");
    }

    public void Dispose()
    {
        m_inventory_service.OnUpdatedMoney -= UpdateMoney;
        m_reinforcer_service.OnUpdatedReinforcement -= UpdateLimit;
    }
}
