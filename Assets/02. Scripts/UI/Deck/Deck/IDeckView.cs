public interface IDeckView
{
    void Inject(DeckPresenter presenter);

    IDeckSlotView InstantiateSlot(); 

    void OpenUI();
    void CloseUI();
    void PlaySFX(string sfx_name);
}