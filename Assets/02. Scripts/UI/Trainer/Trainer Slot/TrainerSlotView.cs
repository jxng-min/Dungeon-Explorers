using TMPro;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainerSlotView : MonoBehaviour, ITrainerSlotView
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 소환 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    private TrainerSlotPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_presenter = new TrainerSlotPresenter(this);
    }

    #region Helper Methods
    public void Initialize(UnitDataBase unit_db, ITrainerInfoView trainer_info_view, InventoryService.Unit unit)
    {
        m_presenter.Initialize(unit_db, trainer_info_view, unit);
        Updates();
    }

    public void Updates()
    {
        m_presenter.UpdateView();
    }

    public void UpdateUI(Sprite unit_sprite, int cost)
    {
        m_unit_image.sprite = unit_sprite;
        m_cost_label.text = NumberFormatter.FormatNumber(cost);
    }
    #endregion Helper Methods

    #region Event Methods
    public void OnPointerClick(PointerEventData eventData)
    {
        m_presenter.OnClickedTrainerSlot();
    }
    #endregion Event Methods
}
