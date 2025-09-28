using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcerSlotView : MonoBehaviour, IReinforcerSlotView
{
    [Header("UI 관련 컴포넌트")]
    [Header("능력치 이름")]
    [SerializeField] private TMP_Text m_reinforcement_name_label;

    [Header("능력치 이미지")]
    [SerializeField] private Image m_reinforcement_image;

    [Header("능력치 레벨")]
    [SerializeField] private TMP_Text m_reinforcement_level_label;

    [Header("능력치 비용")]
    [SerializeField] private TMP_Text m_reinforcement_cost_label;

    [Header("강화 버튼")]
    [SerializeField] private Button m_reinforcement_button;

    [Header("비활성화 패널")]
    [SerializeField] private GameObject m_disabled_panel;

    private ReinforcerSlotPresenter m_presenter;

    private void OnDestroy()
    {
        m_presenter?.Dispose();
    }

    public void Inject(ReinforcerSlotPresenter presenter)
    {
        m_presenter = presenter;

        m_reinforcement_button.onClick.AddListener(m_presenter.OnClickedReinforcement);
    }

    public void UpdateUI(string reinforcement_name, Sprite reinforcement_image)
    {
        m_reinforcement_name_label.text = reinforcement_name;
        m_reinforcement_image.sprite = reinforcement_image;
    }

    public void UpdateReinforcement(int level, bool is_limit)
    {
        m_reinforcement_level_label.text = level.ToString();
        
        m_reinforcement_button.gameObject.SetActive(!is_limit);
        m_disabled_panel.SetActive(is_limit);
    }

    public void UpdateCost(int cost, bool can_purchase)
    {
        m_reinforcement_button.interactable = can_purchase;
        m_reinforcement_cost_label.text = can_purchase ?
                                          $"<color=white>{NumberFormatter.FormatNumber(cost)}</color>" :
                                          $"<color=red>{NumberFormatter.FormatNumber(cost)}</color>"; 
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }
}
