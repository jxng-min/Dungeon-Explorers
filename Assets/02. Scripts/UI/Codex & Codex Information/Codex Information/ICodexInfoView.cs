using UnityEngine;

public interface ICodexInfoView
{
    void OpenUI(Sprite image, string name, string description);
    void CloseUI();
}