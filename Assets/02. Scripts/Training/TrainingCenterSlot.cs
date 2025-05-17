using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingCenterSlot : MonoBehaviour
{
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_explorer_image;

    [Header("탐험가의 소환 비용")]
    [SerializeField] private TMP_Text m_explorer_cost;

    private int m_explorer_id;

    public void Initialize(int id)
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(id);

        m_explorer_id = explorer.ID;
        m_explorer_image.sprite = explorer.Image;
        m_explorer_cost.text = explorer.Cost.ToString();
    }

    public void BUTTON_Info()
    {
        var train_station = FindFirstObjectByType<TrainingStation>();
        train_station.Open(m_explorer_id);
    }
}