using System.Collections.Generic;

public interface IReinforcerDataBase
{
    List<ReinforcementItem> List { get; }
    ReinforcementItem GetItem(ReinforcementType type);
}