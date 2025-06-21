using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Units;

[RequireComponent(typeof(Animator))]
public class Information : MonoBehaviour
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [SerializeField] UnitDataBase m_unit_db;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("탐험가의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("탐험가의 설명 라벨")]
    [SerializeField] private TMP_Text m_description_label;

    private Animator m_information_animator;
    #endregion Variables

    private void Awake()
    {
        m_information_animator = GetComponent<Animator>();
    }

    #region Helper Methods
    private void Initialize(UnitCode code)
    {
        var unit = m_unit_db.GetUnit(code);
        if (unit == null)
        {
            return;
        }

        UpdateUI(unit, code);
    }

    private void UpdateUI(Unit unit, UnitCode code)
    {
        m_unit_image.sprite = unit.Image;
        m_name_label.text = ServiceLocator.Instance.UnitRepoService.GetName(code);
        m_description_label.text = ServiceLocator.Instance.UnitRepoService.GetDescription(code);
    }

    public void OpenUI(UnitCode code)
    {
        m_information_animator.SetBool("Open", true);
        Initialize(code);
    }

    public void CloseUI()
    {
        m_information_animator.SetBool("Open", false);
    }
    #endregion Helper Methods
}
