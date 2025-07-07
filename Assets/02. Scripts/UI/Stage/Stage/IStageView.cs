public interface IStageView
{
    void OpenUI();
    void CloseUI();
    void UpdateUI(int stage, StageState stage_status);
}