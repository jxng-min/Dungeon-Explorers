using System;
using EXPService;
using InventoryService;
using UserService;

public class StatusPresenter : IDisposable
{
    private readonly IStatusView m_view;

    private readonly IInventoryService m_inventory_service;
    private readonly IUserService m_user_service;
    private readonly IEXPService m_exp_service;


    public StatusPresenter(IStatusView view,
                           IInventoryService inventory_service,
                           IUserService user_service,
                           IEXPService exp_service)
    {
        m_view = view;
        m_inventory_service = inventory_service;
        m_user_service = user_service;
        m_exp_service = exp_service;

        m_inventory_service.OnUpdatedMoney += UpdateMoney;
        m_user_service.OnUpdatedLevel += UpdateLevel;

        m_inventory_service.Initialize();
        m_user_service.Initialize();
    }

    public void UpdateLevel(int level, int current_exp)
    {
        var exp_rate = (float)current_exp / (float)m_exp_service.GetEXP(level);
        
        m_view.UpdateLevel(level, exp_rate);
    }

    public void UpdateMoney(int money)
    {
        m_view.UpdateMoney(money);
    }

    public void Dispose()
    {
        m_inventory_service.OnUpdatedMoney -= UpdateMoney;
        m_user_service.OnUpdatedLevel -= UpdateLevel;        
    }
}
