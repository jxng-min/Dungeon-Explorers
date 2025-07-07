using EXPService;
using InventoryService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserDataService;

public class MenuStatusView : MonoBehaviour, IStatusView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("레벨 라벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("경험치 슬라이더")]
    [SerializeField] private Slider m_exp_slider;

    [Header("머니 라벨")]
    [SerializeField] private TMP_Text m_money_label;

    private StatusPresenter m_presenter;

    private IUserDataService m_user_data_system;
    private IInventoryService m_inventory_system;
    private IEXPService m_exp_system;
    #endregion Variables

    private void Awake()
    {
        m_user_data_system = ServiceLocator.Get<IUserDataService>();
        m_inventory_system = ServiceLocator.Get<IInventoryService>();
        m_exp_system = ServiceLocator.Get<IEXPService>();

        m_presenter = new StatusPresenter(this, m_user_data_system, m_inventory_system, m_exp_system);
    }

    private void Update()
    {
        m_presenter.Updates();
    }

    public void SetLV(int lv)
    {
        m_level_label.text = $"LV.{lv}";
    }

    public void SetEXP(float normalized_exp)
    {
        m_exp_slider.value = normalized_exp;
    }

    public void SetMoney(string money)
    {
        m_money_label.text = money;
    }
}
