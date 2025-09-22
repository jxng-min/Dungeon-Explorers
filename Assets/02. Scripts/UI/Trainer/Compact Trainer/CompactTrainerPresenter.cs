using System;
using InventoryService;
using UnitService;

public class CompactTrainerPresenter : IDisposable
{
    private readonly ICompactTrainerView m_view;
    private readonly IInventoryService m_inventory_service;
    private readonly IUnitService m_unit_service;
    private TrainerData m_trainer_data;

    public CompactTrainerPresenter(ICompactTrainerView view,
                                   IInventoryService inventory_service,
                                   IUnitService unit_service)
    {
        m_view = view;
        m_inventory_service = inventory_service;
        m_unit_service = unit_service;

        m_inventory_service.OnUpdatedMoney += UpdateMoney;
        m_inventory_service.OnUpdatedUnit += UpdateLevel;

        m_view.Inject(this);
    }

    public void OpenUI(TrainerData trainer_data)
    {
        if(m_trainer_data != null && m_trainer_data.Hero.Code == trainer_data.Hero.Code)
        {
            return;
        }

        m_trainer_data = trainer_data;

        m_view.OpenUI();
        m_view.UpdateUI(m_unit_service.GetName(m_trainer_data.Hero.Code),
                        m_trainer_data.Hero.Image);
        
        m_inventory_service.Initialize();
    }

    public void CloseUI()
    {
        m_trainer_data = null;

        m_view.CloseUI();
    }

    public void UpdateMoney(int money)
    {
        if(m_trainer_data == null)
        {
            return;
        }

        var unit_data = m_inventory_service.GetUnit(m_trainer_data.Hero.Code);
        
        var total_cost = m_trainer_data.DefaultCost
                            + m_trainer_data.GrowthCost * (unit_data.Upgrade - 1);

        var can_train = total_cost <= money;

        m_view.UpdateCost(total_cost, can_train);
    }

    public void UpdateLevel(UnitData unit_data)
    {
        if(m_trainer_data == null || m_trainer_data.Hero.Code != unit_data.Code)
        {
            return;
        }

        var is_limit = unit_data.Upgrade >= m_trainer_data.Limit;

        m_view.UpdateLevel(unit_data.Upgrade, m_trainer_data.Limit, is_limit);
    }

    public void OnClickedTrain()
    {
        var unit_data = m_inventory_service.GetUnit(m_trainer_data.Hero.Code);
        
        var total_cost = m_trainer_data.DefaultCost
                            + m_trainer_data.GrowthCost * (unit_data.Upgrade - 1);

        m_inventory_service.UpdateMoney(-total_cost);
        m_inventory_service.UpgradeUnit(m_trainer_data.Hero.Code);
    }

    public void Dispose()
    {
        m_inventory_service.OnUpdatedMoney -= UpdateMoney;
        m_inventory_service.OnUpdatedUnit -= UpdateLevel;
    }
}
