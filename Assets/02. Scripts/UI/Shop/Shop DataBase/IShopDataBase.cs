using UnityEngine;

public interface IShopDataBase
{
    public Sprite GetSprite(UnitCode code);
    public int GetCost(UnitCode code);
}