using UnityEngine;
using UnityEngine.UI;

public class TrainerSlotView : MonoBehaviour, ITrainerSlotView
{
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 버튼")]
    [SerializeField] private Button m_unit_button;

    private TrainerSlotPresenter m_presenter;

    private void OnDisable()
    {
        if(m_presenter != null)
        {
            m_unit_button.onClick.RemoveListener(m_presenter.OnClickedCompact);
        }
    }

    public void Inject(TrainerSlotPresenter presenter)
    {
        m_presenter = presenter;

        m_unit_button.onClick.AddListener(m_presenter.OnClickedCompact);
    }

    public void UpdateUI(Sprite unit_image)
    {
        m_unit_image.sprite = unit_image;
    }

    public void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX(sfx_name);
    }
}
