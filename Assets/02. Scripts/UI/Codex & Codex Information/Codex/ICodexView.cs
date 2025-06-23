using Units;

public interface ICodexView
{
    void Initialize(Unit unit);
    void OpenUI();
    void CloseUI();
    void ResetUI();
}