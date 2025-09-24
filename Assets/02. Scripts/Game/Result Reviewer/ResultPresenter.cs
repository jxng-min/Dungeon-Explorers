using System;
using InventoryService;
using UserService;

public class ResultPresenter : IDisposable
{
    private readonly IResultView m_view;
    
    private readonly IStageDataBase m_stage_db;

    private readonly IInventoryService m_inventory_service;
    private readonly IUserService m_user_service;

    private TowerUnit m_enemy_tower;
    private TowerUnit m_hero_tower;

    public ResultPresenter(IResultView view, 
                           IStageDataBase stage_db, 
                           IInventoryService inventory_service, 
                           IUserService user_service)
    {
        m_view = view;

        m_stage_db = stage_db;

        m_inventory_service = inventory_service;
        m_user_service = user_service;

        m_view.Inject(this);
    }

    public void Inject(TowerUnit hero_tower,
                       TowerUnit enemy_tower)
    {
        m_hero_tower = hero_tower;
        m_hero_tower.Health.OnDead += OpenUI;        
        
        m_enemy_tower = enemy_tower;
        m_enemy_tower.Health.OnDead += OpenUI;
    }

    public void OpenUI()
    {
        var success = GameManager.Instance.GameState == GameEventType.GAMECLEAR;

        var stage = m_stage_db.GetStage(m_stage_db.Current);
        var final_money = success ? stage.Gold : stage.Gold / 4;
        var final_exp = success ? stage.EXP : stage.EXP / 4;

        m_inventory_service.UpdateMoney(final_money);
        m_user_service.UpdateLevel(final_exp);

        m_view.OpenUI();
        m_view.UpdateUI(success, final_money, final_exp);
    }

    public void OnClickedRetry()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void OnClickedTitle()
    {
        LoadingManager.Instance.LoadScene("Title");
    }

    public void Dispose()
    {
        m_hero_tower.Health.OnDead -= OpenUI;  
        m_enemy_tower.Health.OnDead -= OpenUI;
    }
}