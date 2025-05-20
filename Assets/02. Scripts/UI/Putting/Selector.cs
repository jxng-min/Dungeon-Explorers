using UnityEngine;

public enum WorkingMode
{
    NONE,
    EQUIPPING,
    DISSOLVING,
}

public class Selector : MonoBehaviour
{
    [Header("캔버스 오브젝트")]
    [SerializeField] private Canvas m_canvas;

    [Header("[장착] 버튼 게임 오브젝트")]
    [SerializeField] private GameObject m_equipment_button;
    
    [Header("[장착 해제] 버튼 게임 오브젝트")]
    [SerializeField] private GameObject m_dissolved_button;

    [Header("버튼의 프레임 게임 오브젝트")]
    [SerializeField] private GameObject m_button_frame;

    [Header("선택 취소 버튼 게임 오브젝트")]
    [SerializeField] private GameObject[] m_back_button;

    [Space(30)][Header("편성 UI 오브젝트")]
    [SerializeField] private Putter m_putter;

    private Explorer m_explorer;
    public Explorer Explorer { get => m_explorer; }

    private WorkingMode m_is_working = WorkingMode.NONE;
    public WorkingMode Working
    { 
        get => m_is_working; 
        set => m_is_working = value;
    }

    private void Start()
    {
        BUTTON_Close();
    }

    public void Open(Explorer explorer, Vector2 touch_position, bool is_inventory)
    {
        m_explorer = explorer;

        CalculateTouchPosition(touch_position);
        m_button_frame.SetActive(true);
        if(is_inventory)
        {
            m_equipment_button.SetActive(true);
        }
        else
        {
            m_dissolved_button.SetActive(true);
        }
            
        foreach(var button in m_back_button)
            button.SetActive(true);
    }

    public void BUTTON_Close()
    {
        Working = WorkingMode.NONE;

        m_explorer = null;

        m_equipment_button.SetActive(false);
        m_dissolved_button.SetActive(false);
        m_button_frame.SetActive(false);

        foreach(var button in m_back_button)
            button.SetActive(false);

        m_putter.SetElectedSlotsGlow(false);
    }

    private void CalculateTouchPosition(Vector2 touch_position)
    {
        var canvas_rect_transform = m_canvas.GetComponent<RectTransform>();
        var rect_transform = m_button_frame.transform as RectTransform;

        Camera ui_camera = m_canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : m_canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas_rect_transform,
            touch_position,
            ui_camera,
            out Vector2 local_position
        );

        if(touch_position.x < Screen.width * 0.15f)
        {
            local_position.x += rect_transform.sizeDelta.x;
        }

        if(touch_position.y < Screen.height * 0.85f)
        {
            local_position.y -= rect_transform.sizeDelta.y;
        }

        rect_transform.anchoredPosition = local_position;
    }

    public void BUTTON_Equipment()
    {
        Working = WorkingMode.EQUIPPING;
        m_putter.SetElectedSlotsGlow(true);
    }

    public void BUTTON_Dissolve()
    {
        Working = WorkingMode.DISSOLVING;
        m_putter.GetElectedSlot(m_explorer).Clear();
        m_putter.GetCandidateSlot(m_explorer).Dissolved();
        BUTTON_Close();
    }
}
