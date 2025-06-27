using System.Collections.Generic;
using InventoryService;
using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ShopView : MonoBehaviour, IShopView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] private UnitDataBase m_model;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("상점 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("상점 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("상점 UI 스크롤 바")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("상점 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("상점 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private ShopPresenter m_presenter;
    private List<IShopSlotView> m_slots;

    private IInventoryService m_inventory;
    private IUnitRepository m_unit_repo;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_inventory = ServiceLocator.Instance.InvenService;
        m_unit_repo = ServiceLocator.Instance.UnitRepoService;

        m_presenter = new ShopPresenter(this, m_model);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);

        m_slots = new();
    }

    private void Start()
    {
        m_presenter.Initialize();
    }

    #region Helper Methods
    public void Initialize(List<Units.Unit> units)
    {
        foreach (var unit in units)
        {
            var shop_slot = Instantiate(m_slot_prefab, m_slot_root).GetComponent<ShopSlotView>();
            m_slots.Add(shop_slot);

            shop_slot.Initialize(this, m_unit_repo, m_inventory, unit);
        }
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
        UpdateUI();
    }

    public void ResetUI()
    {
        m_scroll_bar.value = 0f;
    }

    public void UpdateUI()
    {
        foreach (var slot in m_slots)
        {
            slot.Updates();
        }
    }
    #endregion Helper Methods
}
