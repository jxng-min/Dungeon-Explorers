using System.Collections.Generic;
using InventoryService;

public interface ITrainerView
{
    void InstantiateSlots(List<Unit> unit_list);
    void OpenUI();
    void CloseUI();
    void ResetUI();
}