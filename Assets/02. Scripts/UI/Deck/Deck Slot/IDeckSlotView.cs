using UnityEngine;

public interface IDeckSlotView
{
    void Inject(DeckSlotPresenter presenter);

    void UpdateUI(Sprite unit_image, int unit_cost);
    void UpdateState(bool is_selected);
}