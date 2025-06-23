using System.Collections.Generic;
using Units;

public interface IShopView
{
    void Initialize(List<Unit> unit);
    void OpenUI();
    void CloseUI();
    void ResetUI();
}