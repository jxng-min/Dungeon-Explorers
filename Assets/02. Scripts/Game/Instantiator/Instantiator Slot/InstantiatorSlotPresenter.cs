using UnityEngine;
using ObjectPool;
using Units;

public class InstantiatorSlotPresenter
{
    #region Variables
    private readonly IInstantiatorSlotView m_view;
    private readonly InstantiatorSlotModel m_model;
    #endregion Variables

    public InstantiatorSlotPresenter(IInstantiatorSlotView view)
    {
        m_view = view;
        m_model = new InstantiatorSlotModel();
    }

    public void Initialize(UnitCode code, UnitDataBase unit_db, ICostView cost_view)
    {
        m_model.Initialize(code, unit_db, cost_view);

        m_view.ClearUI();

        if (m_model.Code != UnitCode.EMPTY)
        {
            m_view.InitUI(m_model.Image, m_model.UnitCost);
        }
    }

    public void UpdateView()
    {
        if (m_model.Code != UnitCode.EMPTY)
        {
            m_view.ToggleUI(m_model.UnitCost <= m_model.CurrentCost, m_model.UnitCost);
        }
    }

    public void OnClickedInstantiation()
    {
        m_view.CoolUI(m_model.Cool);
        m_model.UpdateCost(-m_model.UnitCost);

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

        unit.Initialize(m_model.Unit);
    }

    private ObjectType GetObjectType()
    {
        switch (m_model.Unit.Type)
        {
            case UnitType.MELEE:
            case UnitType.GUARD:
                return ObjectType.MELEE_UNIT;

            case UnitType.RANGED:
                return ObjectType.RANGED_UNIT;

            case UnitType.NIMMIA:
                return ObjectType.NIMMIA;

            case UnitType.LELIA:
                return ObjectType.LELIA;
        }

        return ObjectType.NONE;
    }
}
