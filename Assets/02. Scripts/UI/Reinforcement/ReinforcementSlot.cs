using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcementSlot : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("강화 매니저")]
    [SerializeField] private Reinforcer m_reinforcer;

    [Space(50)]
    [Header("슬롯의 타이틀 라벨")]
    [SerializeField] private TMP_Text m_title_label;

    [Header("슬롯의 이미지")]
    [SerializeField] private Image m_reinforcement_image;

    [Header("현재 레벨 라벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("강화 비용 라벨")]
    [SerializeField] private TMP_Text m_reinforcement_label;

    [Header("강화 버튼")]
    [SerializeField] private Button m_reinforcement_button;

    [Header("강화 버튼의 라벨")]
    [SerializeField] private TMP_Text m_button_label;


    [Space(50)]
    [Header("초기화 값 지정")]
    [Header("슬롯의 제목")]
    [SerializeField] private string m_title_string;

    [Header("슬롯의 스프라이트")]
    [SerializeField] private Sprite m_slot_sprite;

    [Header("슬롯의 기본 비용")]
    [SerializeField] private int m_default_cost;

    [Header("강화 당 증가할 비용")]
    [SerializeField] private int m_increase_cost;

    [Header("슬롯의 강화 타입")]
    [SerializeField] ReinforcementType m_type;
    #endregion

    private int m_current_cost;

    private void Awake()
    {
        m_title_label.text = m_title_string;
        m_reinforcement_image.sprite = m_slot_sprite;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        UpdateLevel();
        UpdateCost();
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (m_current_cost > DataManager.Instance.Data.Money)
        {
            m_button_label.text = "<color=red>강화 불가</color>";
            m_reinforcement_button.interactable = false;
        }
        else
        {
            m_button_label.text = "<color=green>강화</color>";
            m_reinforcement_button.interactable = true;
        }
    }

    private void UpdateCost()
    {
        m_current_cost = m_default_cost + m_increase_cost * (m_reinforcer.GetField(m_type) - 1);

        if (m_current_cost > DataManager.Instance.Data.Money)
        {
            m_reinforcement_label.text = $"<color=red>강화 비용: {NumberFormatter.FormatNumber(m_current_cost)}</color>";
        }
        else
        {
            m_reinforcement_label.text = $"<color=white>강화 비용: {NumberFormatter.FormatNumber(m_current_cost)}</color>";
        }
    }

    private void UpdateLevel()
    {
        m_level_label.text = $"LV.{m_reinforcer.GetField(m_type)}";
    }

    public void BUTTON_Reinforcement()
    {
        if (m_current_cost > DataManager.Instance.Data.Money)
        {
            return;
        }

        DataManager.Instance.Data.Money -= m_current_cost;
        m_reinforcer.UpgradeField(m_type);
        m_reinforcer.UpdateSlots();
    }
}
