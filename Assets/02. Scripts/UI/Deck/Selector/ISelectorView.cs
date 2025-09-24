using System.Numerics;

public interface ISelectorView
{
    void Inject(SelectorPresenter presenter);

    void OpenUI(bool is_inventory);
    void CloseUI();
    void SetUIPosition(Vector2 mouse_position);
    void ToggleClose(bool active);
    void ToggleCloseButton(bool active);
    void PlaySFX(string sfx_name);
}