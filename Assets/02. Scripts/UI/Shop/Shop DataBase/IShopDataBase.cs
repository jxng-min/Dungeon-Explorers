using UnityEngine;

public interface IShopDataBase
{
    public ShopData[] List { get; }
    public Sprite GetSprite(UnitCode code);
    public int GetCost(UnitCode code);
}