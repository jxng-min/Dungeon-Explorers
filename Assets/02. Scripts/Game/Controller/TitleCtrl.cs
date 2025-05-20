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

    private void Awake()
    {
        GameEventBus.Publish(GameEventType.WAITING);
    }

    private void Update()
    {
        m_level_label.text = $"LV.{DataManager.Instance.Data.LV}";
        m_exp_slider.value = DataManager.Instance.Data.EXP / ExpManager.Instance.GetExp(DataManager.Instance.Data.LV);
        if (m_exp_slider.value >= 1f)
        {
            DataManager.Instance.Data.EXP -= ExpManager.Instance.GetExp(DataManager.Instance.Data.LV);
            DataManager.Instance.Data.LV++;
        }

        m_money_label.text = NumberFormatter.FormatNumber(DataManager.Instance.Data.Money);
    }
}
