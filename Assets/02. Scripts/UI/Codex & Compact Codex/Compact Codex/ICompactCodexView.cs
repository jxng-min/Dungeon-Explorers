using UnityEngine;

public interface ICompactCodexView
{
    void Inject(CompactCodexPresenter presenter);

    void OpenUI();
    void UpdateUI(Sprite unit_image, string unit_name, string unit_description);
    void CloseUI();
}