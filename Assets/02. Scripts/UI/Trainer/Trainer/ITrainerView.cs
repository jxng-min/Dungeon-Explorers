public interface ITrainerView
{
    void Inject(TrainerPresenter presenter);

    ITrainerSlotView InstantiateSlot();

    void OpenUI();
    void CloseUI();

    void PlaySFX(string sfx_name);
}