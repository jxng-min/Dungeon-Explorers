using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrainingStation : MonoBehaviour
{
    [Header("탐험가의 슬롯")]
    [SerializeField] private TraningCenterSlot m_explorer_slot;

    [Header("탐험가의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("탐험가의 체력 라벨")]
    [SerializeField] private TMP_Text m_hp_label;

    [Header("탐험가의 공격력 라벨")]
    [SerializeField] private TMP_Text m_atk_label;

    [Header("탐험가의 훈련 라벨")]
    [SerializeField] private TMP_Text m_upgrade_label;

    [Header("탐험가의 훈련 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;
    private int m_cost;

    private Animator m_tranining_station_animator;

    private void Awake()
    {
        m_tranining_station_animator = GetComponent<Animator>();
    }

    public void Open(int explorer_id)
    {
        m_tranining_station_animator.SetBool("Open", true);
        Initialize(explorer_id);
    }

    public void BUTTON_Close()
    {
        m_tranining_station_animator.SetBool("Open", false);
    }

    private void Initialize(int id)
    {
        var explorer = ExplorerDataManager.Instance.GetExplorer(id);
        if (explorer == null)
        {
            return;
        }

        m_explorer_slot.Initialize(id);
        m_name_label.text = explorer.Name;
        m_hp_label.text = $"체력: {explorer.HP}";
        m_atk_label.text = $"공격력: {explorer.ATK}";
        // TODO: 업그레이드 단계 출력
    }
}