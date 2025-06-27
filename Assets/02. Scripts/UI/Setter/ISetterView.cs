public interface ISetterView
{
    void OpenUI();
    void CloseUI();
    void UpdateUI(bool bgm, float bgm_value, bool sfx, float sfx_value);

    void SetBGMInteractable(bool interactable);
    void SetSFXInteractable(bool interactable);
}