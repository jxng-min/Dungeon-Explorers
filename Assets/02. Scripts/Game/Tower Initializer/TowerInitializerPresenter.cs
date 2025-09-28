using ReinforcerService;
using UnityEngine;

public class TowerInitializerPresenter
{
    private readonly ITowerInitializerView m_view;
    private readonly IReinforcerService m_reinforcer_service;
    private readonly IStageDataBase m_stage_db;

    private readonly int DEFAULT_HP = 350;
    private readonly int GROWTH_HP = 10;

    public TowerInitializerPresenter(ITowerInitializerView view,
                                     IReinforcerService reinforcer_service,
                                     IStageDataBase stage_db)
    {
        m_view = view;
        m_reinforcer_service = reinforcer_service;
        m_stage_db = stage_db;

        Initialize();
    }

    private void Initialize()
    {
        InitHeroTower();
        InitEnemyTower();
    }

    private void InitHeroTower()
    {
        var upgrade_count = m_reinforcer_service.GetField(ReinforcementType.TOWER_HP);
        
        var final_hp = DEFAULT_HP
                        + GROWTH_HP * (upgrade_count - 1);

        m_view.InitTower(true, final_hp);
    }

    private void InitEnemyTower()
    {
        var current_stage_index = m_stage_db.Current;
        var current_stage = m_stage_db.GetStage(current_stage_index);

        if(current_stage == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"{current_stage_index}에 해당하는 스테이지 정보가 없습니다.");
#endif
            return;            
        }

        var final_hp = current_stage.BaseHP;

        m_view.InitTower(false, final_hp);
    }
}
