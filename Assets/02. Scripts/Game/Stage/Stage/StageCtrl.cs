using System.Collections;
using ObjectPool;
using Units;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    [Header("스테이지 서비스")]
    [SerializeField] private StageService m_stage_service;

    [Header("스폰 위치")]
    [SerializeField] private Transform m_base_transform;

    private Vector2 m_base_position;

    private Stage m_stage;
    private int m_wave_index;
    private float m_timer;

    private bool m_is_dead = false;
    #endregion Variables

    #region Properties
    public bool IsDead { get => m_is_dead; }
    #endregion Properties

    private void Start()
    {
        m_base_position = (Vector2)m_base_transform.position + Vector2.down * 0.9f;
        m_stage = m_stage_service.GetStage(m_stage_db.Stage);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

        UpdateWave();
    }

    #region Helper Methods
    private void UpdateWave()
    {
        m_timer += Time.deltaTime;

        if (m_wave_index < m_stage.Waves.Length)
        {
            var wave = m_stage.Waves[m_wave_index];

            if (m_timer >= wave.SpawnTime)
            {
                StartCoroutine(Co_StartWave(wave));
                m_wave_index++;
            }
        }
    }

    private IEnumerator Co_StartWave(Wave wave)
    {
        for (int i = 0; i < wave.Count; i++)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            Instantiate(wave);

            yield return new WaitForSeconds(wave.Interval);
        }
    }

    private void Instantiate(Wave wave)
    {
        var object_type = GetObjectType(wave.Enemy.Type);

        var unit_obj = ObjectManager.Instance.GetObject(object_type);
        if (unit_obj == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("생성한 적이 null입니다.");
#endif
            return;
        }
        unit_obj.transform.position = m_base_position;

        var unit = unit_obj.GetComponent<BaseUnit>();
        unit.Initialize(wave.Enemy);
    }

    private ObjectType GetObjectType(UnitType type)
    {
        switch (type)
        {
            case UnitType.MELEE:
            case UnitType.GUARD:
                return ObjectType.MELEE_UNIT;

            case UnitType.RANGED:
                return ObjectType.RANGED_UNIT;
        }

        return ObjectType.NONE;
    }
    #endregion Helper Methods
}
