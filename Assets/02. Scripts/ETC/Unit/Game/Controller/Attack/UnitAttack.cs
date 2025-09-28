using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using InventoryService;
using Units;

[RequireComponent(typeof(BaseUnit))]
public class UnitAttack : MonoBehaviour, IAttack
{
    private BaseUnit m_unit;
    private IInventoryService m_inventory_service;


    public int ATK { get; set; }
    public float Range { get; set; }
    public float Interval { get; set; }

    private void Awake()
    {
        m_unit = GetComponent<BaseUnit>();
    }

    private void OnDrawGizmos()
    {
        if(m_unit == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, Range);
    }

    public void Initialize()
    {
        m_inventory_service = ServiceLocator.Get<IInventoryService>();

        ATK = !IsHero() ? m_unit.Unit.ATK :
                          m_unit.Unit.ATK + (m_unit.Unit as Hero).GrowthATK * (m_inventory_service.GetUnit(m_unit.Unit.Code).Upgrade - 1);

        Range = m_unit.Unit.Range;
        Interval = m_unit.Unit.ATKCool;
    }

    public bool CanAttack()
    {
        var mask = 1 << m_unit.Unit.EnemyLayer;
        var hits = Physics2D.OverlapCircleAll(transform.position, Range, mask);
        return hits.Length > 0;
    }

    public Transform GetTarget()
    {
        var mask = 1 << m_unit.Unit.EnemyLayer;
        var hits = Physics2D.OverlapCircleAll(transform.position, Range, mask);

        Transform closest = null;
        var closest_distance = Mathf.Infinity;

        foreach(var hit in hits)
        {
            var distance = Vector3.Distance(transform.position, hit.transform.position);
            if(closest_distance > distance)
            {
                closest_distance = distance;
                closest = hit.transform;
            }
        }

        return closest;
    }

    public void CreateDamageIndicator(Transform target_transform)
    {
        var indicator_obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        indicator_obj.transform.SetParent(target_transform);
        indicator_obj.transform.localPosition = Vector2.up * 0.2f;

        var damage_indicator = indicator_obj.GetComponent<DamageIndicator>();
        damage_indicator.Initialize($"<color=#F6BB43>{NumberFormatter.FormatNumber(ATK)}</color>");
    }

    private bool IsHero()
    {
        var target_enemy_layer = m_unit.Unit.EnemyLayer;
        var enemy_layer = LayerMask.NameToLayer("ENEMY");

        return target_enemy_layer == enemy_layer;
    }
}
