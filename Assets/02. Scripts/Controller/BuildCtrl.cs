using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCtrl : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("스테이지 컨트롤러")]
    [SerializeField] StageCtrl m_stage_ctrl;

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
        // TODO: PLAYING이 아니면 업데이트 안함.
        UpdateUI();
    }


    public void BUTTON_Upgrade()
    {
        m_stage_ctrl.UpdateCurrentCost(-m_upgrade_cost);
        m_upgrade_level++;

        m_stage_ctrl.Interval -= StageCtrl.INTERVAL_UPGRADE;
    }

    private void UpdateUI()
    {
        m_upgrade_cost = m_default_upgrade_cost + (m_default_upgrade_cost * m_upgrade_level);

        if (m_stage_ctrl.Cost >= m_upgrade_cost)
        {
            m_upgrade_button.interactable = true;
            m_cost_label.text = $"<color=green>{m_upgrade_cost}</color>";
        }
        else
        {
            m_upgrade_button.interactable = false;
            m_cost_label.text = $"<color=red>{m_upgrade_cost}</color>";
        }
    }
}
