public interface IShopView
{
    void Inject(ShopPresenter presenter);

    IShopSlotView InstantiateSlot();

    void OpenUI();
    void CloseUI();

    void PlaySFX(string sfx_name);
}