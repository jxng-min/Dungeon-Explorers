using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    [Header("레벨 관련 라벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("경험치 관련 슬라이더")]
    [SerializeField] private Slider m_exp_slider;

    [Header("보유 머니 관련 라벨")]
    [SerializeField] private TMP_Text m_money_label;

    private void Update()
    {
        m_level_label.text = DataManager.Instance.Data.LV.ToString();
        //m_exp_slider.value
        m_money_label.text = DataManager.Instance.Data.Money.ToString();
    }
}
