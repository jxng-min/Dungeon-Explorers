using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrainingCenter : MonoBehaviour
{
    [Header("훈련 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("훈련 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    private Animator m_traninging_center_animator;

    private void Awake()
    {
        m_traninging_center_animator = GetComponent<Animator>();
    }

    public void BUTTON_Open()
    {
        m_traninging_center_animator.SetBool("Open", true);
        Initialize();
    }

    public void BUTTON_Close()
    {
        m_traninging_center_animator.SetBool("Open", false);
    }

    private void Initialize()
    {
        Reset();

        foreach(var item in Inventory.Instance.List)
        {
            var explorer = ExplorerDataManager.Instance.GetExplorer(item.ID);
        
            var obj = Instantiate(m_slot_prefab, m_slot_root);
            var slot = obj.GetComponent<TrainingCenterSlot>();
            slot.Initialize(item.ID);
        }
    }

    // TODO: 오브젝트 풀링
    private void Reset()
    {
        TrainingCenterSlot[] slots = m_slot_root.GetComponentsInChildren<TrainingCenterSlot>();

        foreach(var slot in slots)
        {
            Destroy(slot);
        }
    }
}
