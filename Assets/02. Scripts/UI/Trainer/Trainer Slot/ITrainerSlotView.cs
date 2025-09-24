using UnityEngine;

public interface ITrainerSlotView
{
    void Inject(TrainerSlotPresenter presenter);

    void UpdateUI(Sprite unit_image);
    void PlaySFX(string sfx_name);
}