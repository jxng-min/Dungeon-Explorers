using UnityEngine;

public class CallerCtrl : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("코스트 컨트롤러")]
    [SerializeField] private CostCtrl m_cost_ctrl;

    [Space(30)]
    [Header("콜러들의 부모 트랜스폼")]
    [SerializeField] private Transform m_caller_root;
    #endregion

    private Caller[] m_callers;

    private void Awake()
    {
        m_callers = m_caller_root.GetComponentsInChildren<Caller>();
    }

    private void Start()
    {
        InitializeCaller();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }
        
        UpdateCaller();
    }

    private void InitializeCaller()
    {
        for (int i = 0; i < 5; i++)
        {
            m_callers[i].Initialize(StageManager.Instance.Party[i]);
        }
    }
    
    private void UpdateCaller()
    {
        for (int i = 0; i < 5; i++)
        {
            if (StageManager.Instance.Party[i].ID >= 0)
            {
                m_callers[i].UpdateState(m_cost_ctrl.Cost);
            }
        }
    }
}
