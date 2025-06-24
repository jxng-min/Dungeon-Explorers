using System.Collections.Generic;
using System.Linq;
using InventoryService;
using ReinforcementService;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ReinforcementView : MonoBehaviour, IReinforcementView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] private ReinforceDataBase m_reinforce_db;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [Header("강화 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("강화 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("강화 UI 스크롤 바")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("강화 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("강화 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private ReinforcementPresenter m_presenter;
    private IReinforcementService m_model;
    private IInventoryService m_inventory_service;
    private List<ReinforcementSlotView> m_slots;
    #endregion Variables

    public void Awake()
    {
        m_slots = new();

        m_animator = GetComponent<Animator>();

        m_model = ServiceLocator.Instance.ReinforceService;
        m_inventory_service = ServiceLocator.Instance.InvenService;

        m_presenter = new ReinforcementPresenter(this, m_model);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    public void Start()
    {
        m_presenter.Initialize();   
    }

    #region Helper Methods
    public void Initialize(Dictionary<ReinforcementType, int> reinforcement_dict)
    {
        var reinforcement_list = reinforcement_dict.ToList();
        foreach (var reinforcement_data in reinforcement_list)
        {
            var reinforcement_slot = Instantiate(m_slot_prefab, m_slot_root).GetComponent<ReinforcementSlotView>();
            m_slots.Add(reinforcement_slot);

            reinforcement_slot.Initialize(m_reinforce_db, m_model, m_inventory_service, reinforcement_data.Key);
        }
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
        UpdateUI();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void ResetUI()
    {
        m_scroll_bar.value = 0f;
    }

    private void UpdateUI()
    {
        foreach (var slot in m_slots)
        {
            slot.Updates();
        }
    }
    #endregion Helper Methods
}
