using TMPro;
using UnityEngine;

public class TowerView : MonoBehaviour, ITowerView
{
    #region Variables
    [Header("현재 체력 라벨")]
    [SerializeField] private TMP_Text m_hp_label;

    [Header("최대 체력 라벨")]
    [SerializeField] private TMP_Text m_max_hp_label;
    #endregion Variables

    #region Helper Methods
    public void UpdateUI(float current_hp, float max_hp)
    {
        m_hp_label.text = NumberFormatter.FormatNumber(current_hp);
        m_max_hp_label.text = NumberFormatter.FormatNumber(max_hp);
    }
    #endregion Helper Methods
}
