using UnityEngine;
using UnityEngine.UI;
using Units;

public class DictionarySlot : MonoBehaviour
{
    [Header("도감 슬롯의 이미지")]
    [SerializeField] private Image m_dictionary_image;

    private UnitCode m_unit_code;

    public void Initialize(Unit unit)
    {
        m_unit_code = unit.Code;
        m_dictionary_image.sprite = unit.Image;
    }

    public void BUTTON_Info()
    {
        var info = FindFirstObjectByType<Information>();
        info.OpenUI(m_unit_code);
    }
}
