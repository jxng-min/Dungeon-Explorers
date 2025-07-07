using ReinforcementService;
using UnityEngine;

public class TowerInitializer : MonoBehaviour
{
    #region Variables
    private readonly int DEFAULT_HP = 350;
    private readonly int GROWTH_HP = 10;

    [Header("의존성 관련 컴포넌트")]
    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    [Header("스테이지 서비스")]
    [SerializeField] private StageService m_stage_service;

    [Space(50f)]
    [Header("게임 오브젝트 관련 컴포넌트")]
    [Header("아군 타워 유닛")]
    [SerializeField] private TowerUnit m_hero_tower_unit;

    [Header("적군 타워 유닛")]
    [SerializeField] private TowerUnit m_enemy_tower_unit;

    private IReinforcementService m_reinforcement_system;
    #endregion Variables

    private void Awake()
    {
        m_reinforcement_system = ServiceLocator.Instance.ReinforceService;
    }

    private void Start()
    {
        InitializeHeroTower();
        InitializeEnemyTower();
    }

    #region Helper Methods
    private void InitializeEnemyTower()
    {
        var current_stage_index = m_stage_db.Stage;
        var current_stage = m_stage_service.GetStage(current_stage_index);
        if (current_stage == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"{current_stage_index}에 해당하는 스테이지 정보가 없습니다.");
#endif
            return;
        }

        m_enemy_tower_unit.Initialize(false, current_stage.BaseHP);
    }

    private void InitializeHeroTower()
    {
        var upgrade_count = m_reinforcement_system.GetField(ReinforcementType.TOWER_HP);
        var final_hp = DEFAULT_HP + GROWTH_HP * (upgrade_count - 1);

        m_hero_tower_unit.Initialize(true, final_hp);
    }
    #endregion Helper Methods
}
