using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageIndicator : MonoBehaviour
{
    [Header("캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("데미지 라벨")]
    [SerializeField] private TMP_Text m_damage_label;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_animator.SetTrigger("Up");
    }

    private void OnDisable()
    {
        m_animator.ResetTrigger("Up");
    }

    public void Initialize(float damage)
    {
        m_damage_label.text = NumberFormatter.FormatNumber(damage);
    }

    public void Initialize(string state)
    {
        m_damage_label.text = state;
    }

    public void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.DAMAGE_INDICATOR);
    }
}