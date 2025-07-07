using UnityEngine;
using UnityEngine.UI;
using Units;

public class CodexSlot : MonoBehaviour
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("도감 슬롯의 이미지")]
    [SerializeField] private Image m_codex_image;

    [Header("도감 슬롯의 버튼")]
    [SerializeField] private Button m_codex_button;

    private UnitCode m_unit_code;
    private CodexInfo m_codex_info;
    #endregion Variables

    private void Awake()
    {
        m_codex_button.onClick.AddListener(OnClickedOpenInfoUI);
    }

    #region Helper Methods
    public void Initialize(CodexInfo codex_info, Unit unit)
    {
        m_unit_code = unit.Code;
        m_codex_image.sprite = unit.Image;

        m_codex_info = codex_info;
    }

    public void OnClickedOpenInfoUI()
    {
        m_codex_info.ShowUnitDetail(m_unit_code);
    }
    #endregion Helper Methods
}
