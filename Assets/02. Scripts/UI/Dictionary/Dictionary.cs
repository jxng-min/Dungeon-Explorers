using UnityEngine;
using UnityEngine.UI;
using Units;

[RequireComponent(typeof(Animator))]
public class Dictionary : MonoBehaviour
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("도감 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("도감 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("도감 UI 스크롤 뷰의 바 오브젝트")]
    [SerializeField] private Scrollbar m_scroll_bar;

    private Animator m_dictionary_animator;
    #endregion Variables

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
        for(int i = 0; i < m_unit_db.Count; i++)
        {
            var obj = Instantiate(m_slot_prefab, m_slot_root);

            var dictionary_slot = obj.GetComponent<DictionarySlot>();
            dictionary_slot.Initialize(m_unit_db.GetUnit((UnitCode)i));
        }
    }

    public void OpenUI()
    {
        m_dictionary_animator.SetBool("Open", true);
    }

    public void CloseUI()
    {
        m_dictionary_animator.SetBool("Open", false);
    }

    public void ResetScrollBar()
    {
        m_scroll_bar.value = 0f;
    }
}