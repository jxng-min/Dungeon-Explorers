using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickCtrl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("터치 패널의 트랜스폼")]
    [SerializeField] private RectTransform m_joystick_panel;

    [Header("조이스틱")]
    [SerializeField] private RectTransform m_joystick_handle;

    private float m_joystick_panel_radius;
    
    private Vector2 m_start_position;
    private Vector2 m_input;

    private void Start()
    {
        m_joystick_panel_radius = m_joystick_panel.rect.width / 3f;
        m_joystick_panel.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_start_position = eventData.position;

        m_joystick_panel.position = m_start_position;
        m_joystick_panel.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - m_start_position;
        direction.y = 0;
        
        m_input = Vector2.ClampMagnitude(direction, m_joystick_panel_radius);
        m_joystick_handle.anchoredPosition = m_input;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_input = Vector2.zero;
        
        m_joystick_handle.anchoredPosition = Vector2.zero;
        m_joystick_panel.gameObject.SetActive(false);
    }

    public Vector2 GetInput()
    {
        return m_input != null ? m_input.normalized : Vector2.zero;
    }
}
