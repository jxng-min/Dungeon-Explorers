using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CodexInfo : MonoBehaviour, ICodexInfoView
{
    #region Variables
    [Header("의존성 관련 컴포넌트")]
    [Header("유닛 데이터 스크립터블 오브젝트")]
    [SerializeField] private UnitDataBase m_unit_db;
    private IUnitRepository m_unit_repo;

    [Space(50f)]
    [Header("UI 관련 컴포넌트")]
    [Header("유닛 이미지")]
    [SerializeField] private Image m_unit_image;

    [Header("유닛 이름")]
    [SerializeField] private TMP_Text m_unit_name;

    [Header("유닛 설명")]
    [SerializeField] private TMP_Text m_unit_description;

    [Header("도감 정보 닫기 버튼")]
    [SerializeField] private Button m_close_button;

    private Animator m_animator;
    private CodexInfoPresenter m_presenter;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_unit_repo = ServiceLocator.Get<IUnitRepository>();

        m_presenter = new CodexInfoPresenter(this, m_unit_db, m_unit_repo);
        m_close_button.onClick.AddListener(m_presenter.OnClickedCloseUI);
    }

    #region Helper Methods
    public void ShowUnitDetail(UnitCode code)
    {
        m_presenter.OnClickedOpenUI(code);
    }

    public void OpenUI(Sprite image, string name, string description)
    {
        m_animator.SetBool("Open", true);

        m_unit_image.sprite = image;
        m_unit_name.text = name;
        m_unit_description.text = description;
    }

    public void CloseUI()
    {
        m_animator.SetBool("Open", false);
    }
    #endregion Helper Methods
}
