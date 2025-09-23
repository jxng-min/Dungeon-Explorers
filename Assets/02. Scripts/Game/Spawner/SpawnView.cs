using System.Collections;
using ObjectPool;
using UnityEngine;

public class SpawnView : MonoBehaviour, ISpawnView
{
    [Header("스폰 위치")]
    [SerializeField] private Transform m_spawn_transform;

    private SpawnPresenter m_presenter;

    private void OnDrawGizmos()
    {
        if(m_spawn_transform == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(m_spawn_transform.position, new Vector3(0.35f, 0.37f, 0f));
    }

    public void Inject(SpawnPresenter presenter)
    {
        m_presenter = presenter;
    }

    public void StartWave()
    {
        StartCoroutine(Co_StartWave());
    }

    public void InstantiateEnemy(Wave wave)
    {
        var object_type = GetObjectType(wave.Enemy.Type);

        var unit_obj = ObjectManager.Instance.GetObject(object_type);
        if(unit_obj == null)
        {
            return;
        }

        unit_obj.transform.position = m_spawn_transform.position;

        var unit = unit_obj.GetComponent<BaseUnit>();
        unit.Initialize(wave.Enemy);
    }

    private ObjectType GetObjectType(UnitType type)
    {
        return type switch
        {
            UnitType.MELEE      => ObjectType.MELEE_UNIT,
            UnitType.GUARD      => ObjectType.MELEE_UNIT,
            UnitType.RANGED     => ObjectType.RANGED_UNIT,
            _                   => ObjectType.NONE
        };
    }

    private IEnumerator Co_StartWave()
    {
        while(true)
        {
            m_presenter.Timer += Time.deltaTime;

            if(m_presenter.Current < m_presenter.Last)
            {
                var wave = m_presenter.Wave;

                if(m_presenter.Timer >= wave.SpawnTime)
                {
                    StartCoroutine(Co_UpdateWave(wave));
                    m_presenter.Current++;
                }
            }
            else
            {
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator Co_UpdateWave(Wave wave)
    {
        for(int i = 0; i < wave.Count; i++)
        {
            InstantiateEnemy(wave);

            yield return new WaitForSeconds(wave.Interval);
        }
    }
}
