using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuttingSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("이미지 관련")]
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_explorer_image;

    [Space(30)][Header("소환 비용 관련")]
    [Header("소환 비용의 프레임")]
    [SerializeField] private GameObject m_cost_frame;
    
    [Header("소환 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("선발 출전 알림 라벨")]
    [SerializeField] private TMP_Text m_elected_label;

    private Explorer m_explorer;
    public Explorer Explorer
    {
        get { return m_explorer; }
    }

    private Putter m_putter;

    private Animator m_animator;
    public Animator Animator { get => m_animator; }

    private void Awake()
    {
        m_putter = FindFirstObjectByType<Putter>();
        m_animator = GetComponent<Animator>();
    }

    private void SetAlpha(float alpha)
    {
        var color = m_explorer_image.color;
        color.a = alpha;
        m_explorer_image.color = color;
    }

    public void Clear()
    {
        m_explorer = null;
        
        m_explorer_image.sprite = null;
        
        m_cost_label.text = "";
        m_cost_frame.SetActive(false);

        m_elected_label.gameObject.SetActive(false);

        SetAlpha(0f);
    }

    public void Add(Explorer explorer)
    {
        m_explorer = explorer;

        m_explorer_image.sprite = m_explorer.Image;

        m_cost_label.text = m_explorer.Cost.ToString();
        m_cost_frame.SetActive(true);

        m_elected_label.gameObject.SetActive(false);

        SetAlpha(1f);
    }

    public void Equipped()
    {
        m_elected_label.gameObject.SetActive(true);
    }

    public void Dissolved()
    {
        m_elected_label.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var selector = FindFirstObjectByType<Selector>();
        if(selector == null)
        {
            return;
        }

        if(selector.Working == WorkingMode.EQUIPPING)
        {
            m_putter.SetElectedSlotsGlow(false);
            m_putter.Swap(selector.Explorer, this);

            selector.BUTTON_Close();
            
            return;
        }

        if(m_explorer != null)
        {
            bool is_elected = m_putter.IsAlreadyElected(m_explorer, out int index); 
            selector.Open(m_explorer, eventData.position, !is_elected);
        }
    }
}