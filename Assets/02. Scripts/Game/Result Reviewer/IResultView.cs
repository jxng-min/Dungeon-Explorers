public interface IResultView
{
    void Inject(ResultPresenter presenter);

    void OpenUI();
    void CloseUI();
    void UpdateUI(bool success, int money, int exp);
}