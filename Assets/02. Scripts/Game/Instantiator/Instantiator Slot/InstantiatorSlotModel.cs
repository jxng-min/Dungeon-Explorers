using Units;
using UnityEngine;

public class InstantiatorSlotModel
{
    #region Variables
    private UnitCode m_unit_code;
    private UnitDataBase m_unit_db;
    private ICostView m_cost_view;
    #endregion Variables

    #region Properties
    public UnitCode Code { get => m_unit_code; }
    public Unit Unit { get => m_unit_db.GetUnit(m_unit_code); }
    public Sprite Image { get => m_unit_db.GetUnit(m_unit_code).Image; }
    public int UnitCost { get => (m_unit_db.GetUnit(m_unit_code) as Hero).Cost; }
    public int CurrentCost { get => m_cost_view.GetCost(); }
    public float Cool { get => (m_unit_db.GetUnit(m_unit_code) as Hero).SpawnCool; }
    #endregion Properties

    public void Initialize(UnitCode code, UnitDataBase unit_db, ICostView cost_view)
    {
        m_unit_code = code;
        m_unit_db = unit_db;
        m_cost_view = cost_view;
    }

    public void UpdateCost(int cost)
    {
        m_cost_view.UpdateCost(cost);
    }
}
