using UnityEngine;

public interface IShopSlotView
{
    void Inject(ShopSlotPresenter presenter);
    
    void UpdateUI(string unit_name, Sprite unit_image);
    void UpdatePurchase(int cost, bool can_purchase);
    void UpdateAquire(bool has_unit);
    void PlaySFX(string sfx_name);
}