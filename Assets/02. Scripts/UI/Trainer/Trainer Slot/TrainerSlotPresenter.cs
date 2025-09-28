public class TrainerSlotPresenter
{
    private readonly ITrainerSlotView m_view;
    private readonly CompactTrainerPresenter m_compact_trainer_presenter;
    private readonly TrainerData m_trainer_data;

    public TrainerSlotPresenter(ITrainerSlotView view,
                                TrainerData trainer_data,
                                CompactTrainerPresenter compact_trainer_presenter)
    {
        m_view = view;
        m_trainer_data = trainer_data;
        m_compact_trainer_presenter = compact_trainer_presenter;

        m_view.Inject(this);
        m_view.UpdateUI(m_trainer_data.Hero.Image);
    }

    public void OnClickedCompact()
    {
        m_compact_trainer_presenter.OpenUI(m_trainer_data);
        m_view.PlaySFX("Button Click");
    }
}
