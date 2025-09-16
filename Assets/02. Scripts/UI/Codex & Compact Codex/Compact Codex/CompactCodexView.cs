using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompactCodexView : MonoBehaviour, ICompactCodexView
{
    [Header("UI 관련 컴포넌트")]
    [Header("캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_unit_name_label;

    [Header("유닛 설명")]
    [SerializeField] private TMP_Text m_unit_desc_label;

    [Header("닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Coroutine m_toggle_coroutine;

    private CompactCodexPresenter m_presenter;

    private void OnDestroy()
    {
        m_close_button.onClick.RemoveListener(m_presenter.CloseUI);
    }

    public void Inject(CompactCodexPresenter presenter)
    {
        m_presenter = presenter;

        m_close_button.onClick.AddListener(m_presenter.CloseUI);
    }

    public void OpenUI()
    {
        ToggleCoroutine(true);
    }

    public void UpdateUI(Sprite unit_image, string unit_name, string unit_description)
    {
        m_unit_image.sprite = unit_image;
        m_unit_name_label.text = unit_name;
        m_unit_desc_label.text = unit_description;
    }

    public void CloseUI()
    {
        ToggleCoroutine(false);
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
            m_unit_image.sprite = null;
            m_unit_name_label.text = string.Empty;
            m_unit_desc_label.text = string.Empty;
        }
    }
}
