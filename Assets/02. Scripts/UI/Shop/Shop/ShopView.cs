using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour, IShopView
{
    [Header("UI 관련 컴포넌트")]
    [Header("캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("스크롤 뷰 슬라이더")]
    [SerializeField] private Scrollbar m_scroll_bar;

    [Header("열기 버튼")]
    [SerializeField] private Button m_open_button;

    [Header("닫기 버튼")]
    [SerializeField] private Button[] m_close_buttons;

    [Header("버튼의 이미지")]
    [SerializeField] private Image m_button_image;

    [Space(20f)]
    [Header("슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;   

    private Coroutine m_toggle_coroutine;

    private ShopPresenter m_presenter;

    private void OnDestroy()
    {
        m_open_button.onClick.RemoveListener(m_presenter.OpenUI);

        foreach(var close_button in m_close_buttons)
        {
            close_button.onClick.RemoveListener(m_presenter.CloseUI);
        } 
    } 

    public void Inject(ShopPresenter presenter)
    {
        m_presenter = presenter;

        m_open_button.onClick.AddListener(m_presenter.OpenUI);

        foreach(var close_button in m_close_buttons)
        {
            close_button.onClick.AddListener(m_presenter.CloseUI);
        } 

        m_presenter.Initialize();
    }

    public IShopSlotView InstantiateSlot()
    {
        var slot_obj = Instantiate(m_slot_prefab, m_slot_root);

        return slot_obj.GetComponent<IShopSlotView>();
    }

    public void OpenUI()
    {
        m_button_image.color = Color.yellow;
        ToggleCoroutine(true);
    }

    public void CloseUI()
    {
        m_button_image.color = Color.white;
        ToggleCoroutine(false);
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }

    private void ToggleCoroutine(bool is_open)
    {
        if(m_toggle_coroutine != null)
        {
            StopCoroutine(m_toggle_coroutine);
            m_toggle_coroutine = null;
        }

        m_toggle_coroutine = StartCoroutine(Co_ToggleUI(is_open));
    }

    private IEnumerator Co_ToggleUI(bool is_open)
    {
        m_canvas_group.blocksRaycasts = is_open;
        m_canvas_group.interactable = is_open;

        float elapsed_time = 0f;
        float target_time = 0.5f;

        if(is_open && m_canvas_group.alpha >= 0.9f)
        {
            yield break;
        }

        if(!is_open && m_canvas_group.alpha <= 0.1f)
        {
            yield break;
        }

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            var alpha_delta = elapsed_time / target_time; 
            m_canvas_group.alpha = is_open ? alpha_delta : 1f - alpha_delta;

            yield return null;
        }

        m_canvas_group.alpha = is_open ? 1f : 0f;

        if(!is_open)
        {
            m_scroll_bar.value = 0f;
        }
    }
}
