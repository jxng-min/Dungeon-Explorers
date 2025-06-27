public interface IDeckView
{
    void Initialize();
    void OpenUI();
    void CloseUI();
    void ResetUI();
    void UpdateUI();
    void SetHighlightSlots(bool flag);
}