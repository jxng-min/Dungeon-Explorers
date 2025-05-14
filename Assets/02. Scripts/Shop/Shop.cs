using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shop : MonoBehaviour
{
    [Header("상점 UI의 애니메이터")]
    [SerializeField] private Animator m_shop_animator;

    [Header("상점 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    private ShopSlot[] m_slots;

    private void Awake()
    {
        m_slots = m_slot_root.GetComponentsInChildren<ShopSlot>();
    }

    private void Start()
    {
        InitializeSlots();
    }

    public void BUTTON_Open()
    {
        m_shop_animator.SetBool("Open", true);

        UpdateSlots();
    }

    public void BUTTON_Close()
    {
        m_shop_animator.SetBool("Open", false);
    }

    private void InitializeSlots()
    {
        foreach(var slot in m_slots)
        {
            slot.Initialize();
        }
    }

    public void UpdateSlots()
    {
        foreach(var slot in m_slots)
        {
            slot.UpdateSlot();
        }
    }
}
