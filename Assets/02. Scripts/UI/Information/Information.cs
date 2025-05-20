using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Information : MonoBehaviour
{
    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_explorer_image;

    [Header("탐험가의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("탐험가의 설명 라벨")]
    [SerializeField] private TMP_Text m_description_label;

    private Animator m_information_animator;

    private void Awake()
    {
        m_information_animator = GetComponent<Animator>();
    }

    private void Initialize(int id)
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(id);
        if(explorer == null)
        {
            return;
        }

        m_explorer_image.sprite = explorer.Image;
        m_name_label.text = explorer.Name;
        m_description_label.text = ExplorerDataManager.Instance.GetDescription(id);
    }

    public void Open(int explorer_id)
    {
        m_information_animator.SetBool("Open", true);

        Initialize(explorer_id);
    }

    public void BUTTON_Close()
    {
        m_information_animator.SetBool("Open", false);
    }
}
