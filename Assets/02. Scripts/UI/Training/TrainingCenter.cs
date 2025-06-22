using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TrainingCenter : MonoBehaviour
{
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("훈련 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("훈련 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("훈련소 UI 스크롤 뷰의 바 오브젝트")]
    [SerializeField] private Scrollbar m_scroll_bar;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void OpenUI()
    {
        m_animator.SetBool("Open", true);
        Initialize();
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
        Invoke("Reset", 0.5f);
    }

    private void Initialize()
    {
        foreach(var item in Inventory.Instance.List)
        {
            var explorer = ExplorerDataManager.Instance.GetExplorer(item.ID);
        
            var obj = ObjectManager.Instance.GetObject(ObjectType.TRAIN_SLOT);
            obj.transform.SetParent(m_slot_root, false);

            var slot = obj.GetComponent<TrainingCenterSlot>();
            slot.Initialize(item.ID);
        }
    }

    private void Reset()
    {
        TrainingCenterSlot[] slots = m_slot_root.GetComponentsInChildren<TrainingCenterSlot>();

        Transform pool_container = GameObject.Find("[Training Center Slot] Container").transform;
        foreach(var slot in slots)
        {
            slot.transform.SetParent(pool_container, false);
            ObjectManager.Instance.ReturnObject(slot.gameObject, ObjectType.TRAIN_SLOT);
        }

        ResetScrollBar();
    }
    private void ResetScrollBar()
    {
        m_scroll_bar.value = 0f;
    }
}
