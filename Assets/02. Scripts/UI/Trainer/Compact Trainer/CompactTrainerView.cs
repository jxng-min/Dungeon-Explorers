using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompactTrainerView : MonoBehaviour, ICompactTrainerView
{
    [Header("UI 관련 컴포넌트")]
    [Header("캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_unit_name_label;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 레벨")]
    [SerializeField] private TMP_Text m_unit_level_label;

    [Header("강화 가격")]
    [SerializeField] private TMP_Text m_train_cost_label;

    [Header("강화 버튼")]
    [SerializeField] private Button m_train_button;

    private Coroutine m_fade_coroutine;

    private CompactTrainerPresenter m_presenter;

    private void OnDestroy()
    {
        m_train_button.onClick.RemoveListener(m_presenter.OnClickedTrain);
        m_presenter?.Dispose();
    }

    public void Inject(CompactTrainerPresenter presenter)
    {
        m_presenter = presenter;

        m_train_button.onClick.AddListener(m_presenter.OnClickedTrain);
    }

    public void OpenUI()
    {
        Fade(true);
    }

    public void UpdateUI(string unit_name, Sprite unit_sprite)
    {
        m_unit_name_label.text = unit_name;
        m_unit_image.sprite = unit_sprite;
    }

    public void CloseUI()
    {
        Fade(false);
    }

    public void UpdateCost(int cost, bool can_train)
    {
        m_train_button.interactable = can_train;
        m_train_cost_label.text = can_train ? 
                                  $"<color=white>{NumberFormatter.FormatNumber(cost)}</color>" : 
                                  $"<color=red>{NumberFormatter.FormatNumber(cost)}</color>";
    }

    public void UpdateLevel(int current_level, int max_level, bool is_limit)
    {
        m_unit_level_label.text = $"{current_level} / {max_level}";
        m_train_button.gameObject.SetActive(!is_limit);
    }

    private void Fade(bool is_in)
    {
        if(m_fade_coroutine != null)
        {
            StopCoroutine(m_fade_coroutine);
            m_fade_coroutine = null;
        }

        m_fade_coroutine = StartCoroutine(FadeToggle(is_in));
    }

    private IEnumerator FadeToggle(bool is_in)
    {
        float elapsed_time = 0f;
        float target_time = 0.5f;

        if(is_in && m_canvas_group.alpha >= 0.9f)
        {
            yield break;
        }

        if(!is_in && m_canvas_group.alpha <= 0.1f)
        {
            yield break;
        }

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            var alpha_delta = elapsed_time / target_time;
            m_canvas_group.alpha = is_in? alpha_delta : 1f - alpha_delta;
        
            yield return null;
        }

        m_canvas_group.alpha = is_in ? 1f : 0f;
    }
}
