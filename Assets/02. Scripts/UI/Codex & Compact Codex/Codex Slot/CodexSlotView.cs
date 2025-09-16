using UnityEngine;
using UnityEngine.UI;

public class CodexSlotView : MonoBehaviour, ICodexSlotView
{
    [Header("UI 관련 컴포넌트")]
    [Header("슬롯 버튼")]
    [SerializeField] private Button m_slot_button;

    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image; 

    private CodexSlotPresenter m_presenter;

    private void OnDestroy()
    {
        m_slot_button.onClick.RemoveListener(m_presenter.OpenCompactUI);
    }

    public void Inject(CodexSlotPresenter presenter)
    {
        m_presenter = presenter;

        m_slot_button.onClick.AddListener(m_presenter.OpenCompactUI);
    }

    public void UpdateUI(Sprite unit_image)
    {
        m_unit_image.sprite = unit_image;
    }
}
