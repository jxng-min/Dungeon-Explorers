public interface IReinforcerView
{
    void Inject(ReinforcerPresenter presenter);

    IReinforcerSlotView InstantiateSlot();

    void OpenUI();
    void CloseUI();
}