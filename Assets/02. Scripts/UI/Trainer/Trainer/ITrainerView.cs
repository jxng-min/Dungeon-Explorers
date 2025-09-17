public interface ITrainerView
{
    void Inject(TrainerPresenter presenter);

    ITrainerSlotView InstantiateSlot();

    void OpenUI();
    void CloseUI();
}