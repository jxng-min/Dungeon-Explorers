public interface ITrainerInfoView
{
    void Initialize(InventoryService.Unit unit);
    void OpenUI();
    void CloseUI();
    void UpdateUI(string name, float hp, int atk, int upgrade, int max_upgrade, int cost, int money);
    void ResetUI();
}