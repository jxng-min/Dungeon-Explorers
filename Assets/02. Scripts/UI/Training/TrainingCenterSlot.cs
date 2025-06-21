using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Units;

public class TrainingCenterSlot : MonoBehaviour
{
    #region Variables
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("탐험가의 소환 비용")]
    [SerializeField] private TMP_Text m_unit_cost;

    private UnitCode m_unit_code;
    #endregion Variables

    #region Heler Methods
    public void Initialize(UnitDataBase unit_db, UnitCode code)
    {
        var unit = unit_db.GetUnit(code);

        m_unit_code = unit.Code;
        m_unit_image.sprite = unit.Image;
        m_unit_cost.text = (unit as Hero).Cost.ToString();
    }

    public void BUTTON_Info()
    {
        var train_station = FindFirstObjectByType<TrainingStation>();
        train_station.OpenUI(m_unit_code);
    }
    #endregion Heler Methods
}