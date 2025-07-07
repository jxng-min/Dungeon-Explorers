public interface IDeckView
{
    void Initialize();

    void OpenUI();
    void CloseUI();
    void ResetUI();
    void UpdateUI();
    
    IDeckSlotView GetSlotView(int index);
    void SetHighlightSlots(bool flag);
}