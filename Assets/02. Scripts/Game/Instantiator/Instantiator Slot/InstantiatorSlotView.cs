using System.Collections;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatorSlotView : MonoBehaviour, IInstantiatorSlotView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("생성자 슬롯 버튼")]
    [SerializeField] private Button m_instantiation_button;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("쿨타임 이미지")]
    [SerializeField] private Image m_cooldown_image;

    [Header("코스트 프레임")]
    [SerializeField] private GameObject m_cost_frame;

    [Header("코스트 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    private InstantiatorSlotPresenter m_presenter;
    private Coroutine m_cool_coroutine;
    #endregion Variables

    private void Awake()
    {
        m_presenter = new InstantiatorSlotPresenter(this);

        m_instantiation_button.onClick.AddListener(m_presenter.OnClickedInstantiation);
    }

    public void Initialize(UnitCode code, UnitDataBase unit_db, ICostView cost_view)
    {
        m_presenter.Initialize(code, unit_db, cost_view);
    }

    public void ClearUI()
    {
        m_unit_image.sprite = null;
        SetAlpha(0f);

        m_cooldown_image.gameObject.SetActive(false);

        m_cost_frame.SetActive(false);
        m_cost_label.text = "";

        m_instantiation_button.interactable = false;
    }

    public void InitUI(Sprite unit_sprite, int cost)
    {
        m_unit_image.sprite = unit_sprite;
        SetAlpha(1f);

        m_cost_frame.SetActive(true);
        m_cost_label.text = NumberFormatter.FormatNumber(cost);

        m_instantiation_button.interactable = true;
    }

    private void SetAlpha(float alpha)
    {
        var color = m_unit_image.color;
        color.a = alpha;
        m_unit_image.color = color;
    }

    public void CoolUI(float target_time)
    {
        if (m_cool_coroutine != null)
        {
            StopCoroutine(m_cool_coroutine);
        }

        m_cool_coroutine = StartCoroutine(Co_CoolUI(target_time));
    }

    public void UpdateUI()
    {
        m_presenter.UpdateView();
    }

    public void ToggleUI(bool active, float unit_cost)
    {
        if (active)
        {
            m_cost_label.text = NumberFormatter.FormatNumber(unit_cost);

            m_instantiation_button.interactable = true;
        }
        else
        {
            m_cost_label.text = $"<color=red>{NumberFormatter.FormatNumber(unit_cost)}</color>";

            m_instantiation_button.interactable = false;
        }
    }

    private IEnumerator Co_CoolUI(float target_time)
    {
        m_instantiation_button.interactable = false;

        m_cooldown_image.gameObject.SetActive(true);
        m_cooldown_image.fillAmount = 1f;

        float elapsed_time = 0f;

        while (elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float delta = elapsed_time / target_time;
            m_cooldown_image.fillAmount = 1f - delta;
        }

        m_cooldown_image.fillAmount = 0f;
        m_cooldown_image.gameObject.SetActive(false);

        m_instantiation_button.interactable = true;

        m_cool_coroutine = null;
    }
}
