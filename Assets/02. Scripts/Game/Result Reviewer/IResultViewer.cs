public interface IResultViewer
{
    void OpenUI();
    void OpenView();
    void UpdateUI(bool success, int money, int exp);
}