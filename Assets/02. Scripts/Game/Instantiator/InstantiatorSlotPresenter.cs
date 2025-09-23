using UnityEngine;
using ObjectPool;
using UnitService;
using Units;
using System;

public class InstantiatorSlotPresenter : IDisposable
{
    private readonly IInstantiatorSlotView m_view;

    private readonly CostPresenter m_cost_presenter;

    private readonly IUnitDataBase m_unit_db;
    private readonly UnitCode m_unit_code;

    public InstantiatorSlotPresenter(IInstantiatorSlotView view,
                                     IUnitDataBase unit_db,
                                     UnitCode unit_code,
                                     CostPresenter cost_presenter)
    {
        m_view = view;

        m_unit_db = unit_db;
        m_unit_code = unit_code;

        m_cost_presenter = cost_presenter;
        m_cost_presenter.OnUpdatedCost += UpdateCost;

        m_view.Inject(this);
        Initialize();
    }

    public void Initialize()
    {
        m_view.ClearUI();

        if (m_unit_code != UnitCode.EMPTY)
        {
            var hero = m_unit_db.GetUnit(m_unit_code) as Hero;
            m_view.InitUI(hero.Image, hero.Cost);
        }
    }

    public void UpdateCost(int cost)
    {
        var unit = m_unit_db.GetUnit(m_unit_code);
        var unit_cost = (unit as Hero).Cost;

        m_view.UpdateUI(unit_cost <= cost, unit_cost);
    }

    public void ClickUI()
    {
        var unit = m_unit_db.GetUnit(m_unit_code);

        var unit_cool = (unit as Hero).SpawnCool;
        m_view.CoolUI(unit_cool);
        
        var unit_cost = (unit as Hero).Cost;
        m_cost_presenter.UpdateCost(-unit_cost);

        InstantiateUnit();
    }

    private void InstantiateUnit()
    {
        var object_type = GetObjectType();

        var unit_obj = ObjectManager.Instance.GetObject(object_type);
        unit_obj.transform.position = new Vector3(-8f, -3f, 0f);

        var unit = unit_obj.GetComponent<BaseUnit>();
        if (!unit)
        {
            ObjectManager.Instance.ReturnObject(unit_obj, object_type);
            return;
        }

        unit.Initialize(m_unit_db.GetUnit(m_unit_code));
    }

    private ObjectType GetObjectType()
    {
        var unit = m_unit_db.GetUnit(m_unit_code);
        var unit_type = unit.Type;

        return unit_type switch
        {
            UnitType.MELEE      => ObjectType.MELEE_UNIT,
            UnitType.RANGED     => ObjectType.RANGED_UNIT,
            UnitType.GUARD      => ObjectType.MELEE_UNIT,
            UnitType.NIMMIA     => ObjectType.NIMMIA,
            UnitType.LELIA      => ObjectType.LELIA,
            _                   => ObjectType.NONE
        };
    }

    public void Dispose()
    {
        m_cost_presenter.OnUpdatedCost -= UpdateCost;
    }
}
