public interface IDeckView
{
    void Inject(DeckPresenter presenter);

    IDeckSlotView InstantiateSlot(); 

    void OpenUI();
    void CloseUI();
}