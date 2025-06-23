using Units;

public class ShopPresenter
{
    #region Variables
    private readonly IShopView m_view;
    private readonly UnitDataBase m_model;
    #endregion Variables

    public ShopPresenter(IShopView view, UnitDataBase model)
    {
        m_view = view;
        m_model = model;
    }

    public void Initialize()
    {
        m_view.Initialize(m_model.List);
    }

    public void OnClickedOpenUI()
    {
        m_view.OpenUI();
    }

    public void OnClickedCloseUI()
    {
        m_view.CloseUI();
    }
}
