using UnityEngine;

public interface ICodexSlotView
{
    void Inject(CodexSlotPresenter presenter);

    void UpdateUI(Sprite unit_image);
    void PlaySFX(string sfx_name);
}