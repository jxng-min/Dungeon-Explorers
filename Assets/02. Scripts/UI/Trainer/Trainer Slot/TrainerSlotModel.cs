using Units;
using UnityEngine;

public class TrainerSlotModel
{
    #region Variables
    private UnitDataBase m_unit_db;
    private InventoryService.Unit m_unit;
    private ITrainerInfoView m_trainer_info_view;
    #endregion Variables

    #region Properties
    public Sprite Image { get => m_unit_db.GetUnit(m_unit.Code).Image; }
    public int Cost { get => (m_unit_db.GetUnit(m_unit.Code) as Hero).Cost; }
    public ITrainerInfoView InfoView { get => m_trainer_info_view; }
    public InventoryService.Unit Unit { get => m_unit; }
    #endregion Properties

    #region Helper Methods
    public void Initialize(UnitDataBase unit_db, ITrainerInfoView trainer_info_view, InventoryService.Unit unit)
    {
        m_unit_db = unit_db;
        m_unit = unit;
        m_trainer_info_view = trainer_info_view;
    }
    #endregion Helper Methods
}
