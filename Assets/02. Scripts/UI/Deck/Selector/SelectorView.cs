using UnityEngine;
using UnityEngine.UI;

public class SelectorView : MonoBehaviour, ISelectorView
{
    [Header("UI 관련 컴포넌트")]
    [Header("선택자 오브젝트")]
    [SerializeField] private GameObject m_selector_ui;

    [Header("편성 버튼")]
    [SerializeField] private Button m_enable_button;

    [Header("편성 해제 버튼")]
    [SerializeField] private Button m_disable_button;

    [Header("닫기 버튼 목록")]
    [SerializeField] private Button[] m_close_buttons;

    [Header("닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private SelectorPresenter m_presenter;

    private void OnDestroy()
    {
        m_enable_button.onClick.RemoveListener(m_presenter.OnClickEnable);
        m_disable_button.onClick.RemoveListener(m_presenter.OnClickDisable);

        foreach(var close_button in m_close_buttons)
        {
            close_button.onClick.RemoveListener(m_presenter.CloseUI);
        }        

        m_close_button.onClick.RemoveListener(m_presenter.CloseUI);
    }

    public void Inject(SelectorPresenter presenter)
    {
        m_presenter = presenter;

        m_enable_button.onClick.AddListener(m_presenter.OnClickEnable);
        m_disable_button.onClick.AddListener(m_presenter.OnClickDisable);

        foreach(var close_button in m_close_buttons)
        {
            close_button.onClick.AddListener(m_presenter.CloseUI);
        }

        m_close_button.onClick.AddListener(m_presenter.CloseUI);
    }

    public void OpenUI(bool is_inventory)
    {
        m_selector_ui.SetActive(true);
        ToggleCloseButton(true);

        m_enable_button.interactable = !is_inventory;
        m_disable_button.interactable = is_inventory;
    }

    public void CloseUI()
    {
        m_selector_ui.SetActive(false);
        ToggleCloseButton(false);
    }

    public void SetUIPosition(System.Numerics.Vector2 mouse_position)
    {
        var ui_mouse_position = new Vector2(mouse_position.X, mouse_position.Y);
        m_selector_ui.transform.position = ui_mouse_position;
    }

    public void ToggleClose(bool active)
    {
        foreach(var close_button in m_close_buttons)
        {
            close_button.gameObject.SetActive(active);
        }        
    }

    public void ToggleCloseButton(bool active)
    {
        m_close_button.gameObject.SetActive(active);
    }
}
