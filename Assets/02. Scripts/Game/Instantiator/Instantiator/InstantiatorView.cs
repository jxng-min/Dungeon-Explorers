using Units;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiatorView : MonoBehaviour, IInstantiatorView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("스테이지 데이터베이스")]
    [SerializeField] private StageDataBase m_stage_db;

    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("생성자 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    private IInstantiatorSlotView[] m_slots;
    private InstantiatorPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_presenter = new InstantiatorPresenter(this, m_stage_db);
    }

    private void Start()
    {
        m_presenter.Initialize();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].UpdateUI();
        }
    }

    #region Helper Methods
    public void InitializeSlots(UnitCode[] deck)
    {
        m_slots = m_slot_root.GetComponentsInChildren<IInstantiatorSlotView>();

        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].Initialize(deck[i], m_unit_db);
        }
    }
    #endregion Helper Methods
}
