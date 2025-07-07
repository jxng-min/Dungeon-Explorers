using Units;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITrainerSlotView : IPointerClickHandler
{
    void Initialize(UnitDataBase unit_db, ITrainerInfoView trainer_info_view, InventoryService.Unit unit);
    void UpdateUI(Sprite unit_sprite, int cost);
}