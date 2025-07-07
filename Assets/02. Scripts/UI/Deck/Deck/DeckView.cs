using DeckService;
using InventoryService;
using UnityEngine;
using UnityEngine.UI;
using ObjectPool;
using System.Collections.Generic;
using Units;

[RequireComponent(typeof(Animator))]
public class DeckView : MonoBehaviour, IDeckView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    [Header("덱 편성 도우미")]
    [SerializeField] private SelectorView m_selector_view;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("선택된 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_selected_slot_root;
    private IDeckSlotView[] m_selected_slots;

    [Header("후보 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_candidate_slot_root;
    private List<GameObject> m_candidate_slots;

    [Header("편성 UI 스크롤 바")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("UI 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private DeckPresenter m_presenter;
    private IDeckService m_deck_system;
    private IInventoryService m_inven_system;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_deck_system = ServiceLocator.Get<IDeckService>();
        m_inven_system = ServiceLocator.Get<IInventoryService>();

        m_presenter = new DeckPresenter(this, m_deck_system, m_stage_db);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    #region Helper Methods
    public void Initialize()
    {
        ConfigureSelecteds();
        ConfigureCandidates();
    }

    private void ConfigureSelecteds()
    {
        m_selected_slots = m_selected_slot_root.GetComponentsInChildren<IDeckSlotView>();

        var deck = m_deck_system.GetDeck();
        for (int i = 0; i < m_selected_slots.Length; i++)
        {
            m_selected_slots[i].Initialize(m_unit_db, m_deck_system, this, m_selector_view, deck[i]);
        }
    }

    private void ConfigureCandidates()
    {
        m_candidate_slots = new();

        var deck = m_inven_system.Units;
        for (int i = 0; i < deck.Count; i++)
        {
            var deck_slot_obj = ObjectManager.Instance.GetObject(ObjectType.DECK_SLOT);
            deck_slot_obj.transform.SetParent(m_candidate_slot_root, false);

            var deck_slot = deck_slot_obj.GetComponent<IDeckSlotView>();
            deck_slot.Initialize(m_unit_db, m_deck_system, this, m_selector_view, deck[i].Code);

            m_candidate_slots.Add(deck_slot_obj);
        }
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);

        m_presenter.Initialize();
        UpdateUI();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void UpdateUI()
    {
        foreach (var selected_slot in m_selected_slots)
        {
            selected_slot.Updates();
        }

        foreach (var deck_slot_obj in m_candidate_slots)
        {
            var deck_slot = deck_slot_obj.GetComponent<IDeckSlotView>();
            deck_slot.Updates();
        }
    }

    public void ResetUI()
    {
        ReturnCandidateSlotsToPool();
        m_scroll_bar.value = 0f;
    }

    private void ReturnCandidateSlotsToPool()
    {
        foreach (var deck_slot in m_candidate_slots)
        {
            var origin_slot_root = ObjectManager.Instance.GetPool(ObjectType.DECK_SLOT).Container;
            deck_slot.transform.SetParent(origin_slot_root, false);

            ObjectManager.Instance.ReturnObject(deck_slot, ObjectType.DECK_SLOT);
        }
    }

    public void SetHighlightSlots(bool flag)
    {
        foreach (var slot in m_selected_slots)
        {
            slot.SetHighlight(flag);
        }
    }

    public IDeckSlotView GetSlotView(int index)
    {
        return m_selected_slots[index];
    }
    #endregion Helper Methods
}
