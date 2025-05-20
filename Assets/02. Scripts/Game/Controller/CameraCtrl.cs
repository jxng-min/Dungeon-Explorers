using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [Header("조이스틱")]
    [SerializeField] private JoystickCtrl m_joystick;

    [Header("이동 속도")]
    [SerializeField] private float m_speed =5f;

    [Header("X축의 최소 범위")]
    [Range(0f, 100f)][SerializeField] private float m_min_x = 0f;

    [Header("X축의 최대 범위")]
    [Range(0f, 100f)][SerializeField] private float m_max_x = 100f; 

    private void Start()
    {
        transform.position = Vector2.zero;
    }

    private void Update()
    {
        if(GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

        Vector2 input = m_joystick.GetInput();

        float delta_x = input.x * m_speed * Time.deltaTime;
        float new_x = Mathf.Clamp(transform.position.x + delta_x, m_min_x, m_max_x);

        transform.position = new Vector3(new_x, transform.position.y, -10);
    }
}
