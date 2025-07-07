using System.Collections.Generic;

public interface IReinforcementView
{
    void Initialize(Dictionary<ReinforcementType, int> reinforcement_dict);
    void OpenUI();
    void CloseUI();
    void ResetUI();
}