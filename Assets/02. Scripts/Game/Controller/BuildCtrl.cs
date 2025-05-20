using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCtrl : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("코스트 컨트롤러")]
    [SerializeField] CostCtrl m_cost_ctrl;

    [Space(30)]
    [Header("건물 업그레이드 버튼")]
    [SerializeField] private Button m_upgrade_button;

    [Header("업그레이드 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;
    #endregion

    private int m_default_upgrade_cost = 10;

    private int m_upgrade_level = 0;
    private int m_upgrade_cost = 0;

    private void Update()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }
        UpdateUI();
    }


    public void BUTTON_Upgrade()
    {
        m_cost_ctrl.UpdateCurrentCost(-m_upgrade_cost);
        m_upgrade_level++;

        m_cost_ctrl.Interval -= CostCtrl.INTERVAL_UPGRADE;
    }

    private void UpdateUI()
    {
        m_upgrade_cost = m_default_upgrade_cost + (m_default_upgrade_cost * m_upgrade_level);

        if (m_cost_ctrl.Cost >= m_upgrade_cost)
        {
            m_upgrade_button.interactable = true;
            m_cost_label.text = $"<color=green>{NumberFormatter.FormatNumber(m_upgrade_cost)}</color>";
        }
        else
        {
            m_upgrade_button.interactable = false;
            m_cost_label.text = $"<color=red>{NumberFormatter.FormatNumber(m_upgrade_cost)}</color>";
        }
    }
}
