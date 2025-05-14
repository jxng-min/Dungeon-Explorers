using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dictionary : MonoBehaviour
{
    [Header("도감 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("도감 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    private Animator m_dictionary_animator;

    private void Awake()
    {
        m_dictionary_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for(int i = 0; i < ExplorerDataManager.Instance.GetSize(); i++)
        {
            var obj = Instantiate(m_slot_prefab, m_slot_root);

            var dictionary_slot = obj.GetComponent<DictionarySlot>();
            dictionary_slot.Initialize(ExplorerDataManager.Instance.GetExplorer(i));
        }
    }

    public void BUTTON_Open()
    {
        m_dictionary_animator.SetBool("Open", true);
    }

    public void BUTTON_Close()
    {
        m_dictionary_animator.SetBool("Open", false);
    }
}