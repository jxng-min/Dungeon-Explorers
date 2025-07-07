using System.Collections.Generic;
using InventoryService;
using ObjectPool;
using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TrainerView : MonoBehaviour, ITrainerView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Header("훈련 정보 창")]
    [SerializeField] private TrainerInfoView m_trainer_info_view;

    [Header("UI 관련 컴포넌트")]
    [Header("훈련 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("훈련 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("UI 스크롤 바")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("UI 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("UI 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private TrainerPresenter m_presenter;
    private IInventoryService m_inventory_system;
    private List<GameObject> m_slots;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_inventory_system = ServiceLocator.Get<IInventoryService>();

        m_presenter = new TrainerPresenter(this, m_inventory_system);

        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);

        m_slots = new();
    }

    public void InstantiateSlots(List<InventoryService.Unit> unit_list)
    {
        m_slots.Clear();

        foreach (var unit_item in unit_list)
        {
            var slot_obj = ObjectManager.Instance.GetObject(ObjectType.TRANING_SLOT);
            slot_obj.transform.SetParent(m_slot_root, false);
            m_slots.Add(slot_obj);

            var training_slot = slot_obj.GetComponent<ITrainerSlotView>();
            training_slot.Initialize(m_unit_db, m_trainer_info_view, unit_item);
        }
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    public void ResetUI()
    {
        m_scroll_bar.value = 0f;
        ReturnSlotsToPool();
        m_slots.Clear();
    }

    private void ReturnSlotsToPool()
    {
        foreach (var slot_obj in m_slots)
        {
            var container = ObjectManager.Instance.GetPool(ObjectType.TRANING_SLOT).Container;
            slot_obj.transform.SetParent(container, false);

            ObjectManager.Instance.ReturnObject(slot_obj, ObjectType.TRANING_SLOT);
        }
    }
}
