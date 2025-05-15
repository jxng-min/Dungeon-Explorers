using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Dictionary : MonoBehaviour
{
    [Header("도감 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("도감 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("도감 UI 스크롤 뷰의 바 오브젝트")]
    [SerializeField] private Scrollbar m_scroll_bar;

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
        Invoke("ResetScrollBar", 0.5f);
    }

    private void ResetScrollBar()
    {
        m_scroll_bar.value = 0f;
    }
}