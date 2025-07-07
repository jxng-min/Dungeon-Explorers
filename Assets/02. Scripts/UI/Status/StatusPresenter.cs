using EXPService;
using InventoryService;
using UserDataService;

public class StatusPresenter
{
    #region Variables
    private readonly IStatusView m_view;
    private readonly StatusModel m_model;
    #endregion Variables

    public StatusPresenter(IStatusView view, IUserDataService user_data_system, IInventoryService inventory_system, IEXPService exp_system)
    {
        m_view = view;
        m_model = new StatusModel(user_data_system, inventory_system, exp_system);
    }

    public void Updates()
    {
        int lv = m_model.UserDataSystem.Level;
        int current_exp = m_model.UserDataSystem.EXP;
        int required_exp = m_model.EXPSystem.GetEXP(lv);

        while (current_exp >= required_exp)
        {
            current_exp -= required_exp;
            required_exp = m_model.EXPSystem.GetEXP(++lv);
        }

        m_model.UserDataSystem.Level = lv;
        m_model.UserDataSystem.EXP = current_exp;

        float normalized_exp = (float)current_exp / required_exp;

        m_view.SetLV(lv);
        m_view.SetEXP(normalized_exp);
        m_view.SetMoney(NumberFormatter.FormatNumber(m_model.InventorySystem.Money));
    }
}
