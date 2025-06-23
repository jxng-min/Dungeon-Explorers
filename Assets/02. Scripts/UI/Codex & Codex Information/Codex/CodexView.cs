using UnityEngine;
using UnityEngine.UI;
using Units;

[RequireComponent(typeof(Animator))]
public class CodexView : MonoBehaviour, ICodexView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 데이터 스크립터블 오브젝트")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)][Header("UI 관련 컴포넌트")]
    [Header("도감 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("도감 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    [Header("도감 UI 스크롤 바")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("도감 열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("도감 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    [Space(50f)]
    [Header("CondexInfo 컴포넌트")]
    [SerializeField] private CodexInfo m_codex_info; 

    private Animator m_dictionary_animator;
    private CodexPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_dictionary_animator = GetComponent<Animator>();

        m_presenter = new CodexPresenter(this, m_unit_db);
        
        m_open_button.onClick.AddListener(m_presenter.OnClickedOpenUI);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    private void Start()
    {
        m_presenter.Initialize();
    }

    #region Helper Methods
    public void Initialize(Unit unit)
    {
        var obj = Instantiate(m_slot_prefab, m_slot_root);

        var slot = obj.GetComponent<CodexSlot>();
        slot.Initialize(m_codex_info, unit);
    }

    public void OpenUI()
    {
        m_dictionary_animator.SetBool("Open", true);
    }

    public void CloseUI()
    {
        m_dictionary_animator.SetBool("Open", false);
    }

    public void Reset()
    {
        m_scroll_bar.value = 0f;
    }
    #endregion Helper Methods
}