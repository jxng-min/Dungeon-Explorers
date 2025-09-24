public interface IStageView
{
    void Inject(StagePresenter presenter);

    void OpenUI();
    void UpdateUI(int stage, string state_text);
    void CloseUI();

    void PlaySFX(string sfx_name);
}