using Units;

public interface ICodexView
{
    void Inject(CodexPresenter presenter);
    void OpenUI();
    void CloseUI();
    ICodexSlotView InstantiateSlot();
}