using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    #region 적 생성 관련
    [Header("스테이지 데이터")]
    [SerializeField] private Stage m_stage;
    public Stage Stage { get => m_stage; }

    [Header("적 스폰 위치")]
    [SerializeField] private Transform m_base_transform;
    private Vector3 m_base_position;
    #endregion

    private float m_timer;
    private int m_wave_index = 0;

    private bool m_is_dead = false;
    public bool IsDead { get => m_is_dead; }

    private void Awake()
    {
        m_stage = StageManager.Instance.Current;
    }

    private void Start()
    {
        m_base_position = m_base_transform.position + Vector3.down * 0.9f;
        m_timer = 0f;
    }

    private void Update()
    {
        UpdateWave();   
    }

    private void UpdateWave()
    {
        m_timer += Time.deltaTime;

        if (m_wave_index < m_stage.Waves.Length)
        {
            var wave = m_stage.Waves[m_wave_index];

            if (m_timer >= wave.SpawnTime)
            {
                StartCoroutine(Co_SpawnWave(wave));
                m_wave_index++;
            }
        }
    }

    private IEnumerator Co_SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.Count; i++)
        {
            Instantiate(wave);

            yield return new WaitForSeconds(wave.Interval);
        }
    }

    private void Instantiate(Wave wave)
    {
        var enemy_obj = ObjectManager.Instance.GetObject(ObjectType.ENEMY);
        enemy_obj.transform.position = m_base_position;

        var enemy_ctrl = enemy_obj.GetComponent<EnemyCtrl>();
        if (enemy_ctrl != null)
        {
            Destroy(enemy_ctrl);
        }

        switch (wave.Enemy.Type)
        {
            case EnemyType.Melee:
                enemy_ctrl = enemy_obj.AddComponent<MeleeEnemyCtrl>();
                break;

            case EnemyType.Ranged:
                enemy_ctrl = enemy_obj.AddComponent<RangedEnemyCtrl>();
                break;
        }

        enemy_ctrl.Animator.runtimeAnimatorController = wave.Enemy.Animator;
        enemy_ctrl.Script = wave.Enemy;
        enemy_ctrl.Initialize();
    }
}