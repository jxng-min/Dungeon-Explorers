using UnityEngine;
using UnityEngine.UI;

public class DictionarySlot : MonoBehaviour
{
    [Header("도감 슬롯의 이미지")]
    [SerializeField] private Image m_dictionary_image;

    private int m_explorer_id;

    public void Initialize(Explorer explorer)
    {
        m_explorer_id = explorer.ID;
        m_dictionary_image.sprite = explorer.Image;
    }

    public void BUTTON_Info()
    {
        var info = FindFirstObjectByType<Information>();
        info.Open(m_explorer_id);
    }
}
