using Units;
using UnityEngine;

public class InstantiatorSlotModel
{
    #region Variables
    private UnitCode m_unit_code;
    private UnitDataBase m_unit_db;
    #endregion Variables

    #region Properties
    public UnitCode Code { get => m_unit_code; }
    public Sprite Image { get => m_unit_db.GetUnit(m_unit_code).Image; }
    public int Cost { get => (m_unit_db.GetUnit(m_unit_code) as Hero).Cost; }
    public float Cool { get => (m_unit_db.GetUnit(m_unit_code) as Hero).SpawnCool; }
    #endregion Properties

    public void Initialize(UnitCode code, UnitDataBase unit_db)
    {
        m_unit_code = code;
        m_unit_db = unit_db;
    }
}
