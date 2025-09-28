using UnityEngine;

public interface ISelectedDeckSlotView
{
    void Inject(SelectedDeckSlotPresenter presenter);

    void UpdateUI(Sprite unit_image, int unit_cost);
    void SetHighlight(bool active);
    void PlaySFX(string sfx_name);
}