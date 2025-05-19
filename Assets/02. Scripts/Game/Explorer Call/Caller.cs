using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Caller : MonoBehaviour
{
    [Header("버튼 게임 오브젝트")]
    [SerializeField] private Button m_button;

    [Header("탐험가의 이미지")]
    [SerializeField] private Image m_explorer_image;

    [Header("탐험가의 비용 라벨 프레임")]
    [SerializeField] private Image m_cost_frame;

    [Header("탐험가의 비용 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("탐험가의 쿨타임 이미지")]
    [SerializeField] private Image m_cooltime_image;
    private float m_cooltime;

    private Explorer m_explorer;
    private int m_upgrade_count;

    private CostCtrl m_cost_ctrl;

    private Coroutine m_cooldown_coroutine;

    private ExplorerFactory m_factory;

    private void Awake()
    {
        m_cost_ctrl = FindFirstObjectByType<CostCtrl>();
        m_factory = FindFirstObjectByType<ExplorerFactory>();
    }

    private void SetAlpha(float alpha)
    {
        var color = m_explorer_image.color;
        color.a = alpha;
        m_explorer_image.color = color;
    }

    public void Initialize(ExplorerInfo explorer_info)
    {
        if (explorer_info.ID < 0)
        {
            Clear();
            return;
        }

        Explorer explorer = ExplorerDataManager.Instance.GetExplorer(explorer_info.ID);
        m_explorer = explorer;
        m_upgrade_count = explorer_info.Upgrade;

        m_explorer_image.sprite = m_explorer.Image;

        m_cost_frame.gameObject.SetActive(true);
        m_cost_label.text = NumberFormatter.FormatNumber(m_explorer.Cost);

        m_cooltime_image.fillAmount = 1f;
        m_cooltime_image.gameObject.SetActive(false);

        m_cooltime = m_explorer.CoolTime;

        m_button.interactable = true;

        SetAlpha(1f);
    }

    public void Clear()
    {
        m_button.interactable = false;
        m_explorer_image.sprite = null;
        m_cost_frame.gameObject.SetActive(false);
        m_cost_label.text = "";
        m_cooltime_image.fillAmount = 1f;
        m_cooltime_image.gameObject.SetActive(false);

        SetAlpha(0f);
    }

    public void UpdateState(float cost)
    {
        if (cost >= m_explorer.Cost)
        {
            NonBlock();
        }
        else
        {
            Block();
        }
    }

    private void Block()
    {
        m_cost_label.text = $"<color=red>{NumberFormatter.FormatNumber(m_explorer.Cost)}</color>";

        m_button.interactable = false;
    }

    private void NonBlock()
    {
        m_cost_label.text = $"<color=white>{NumberFormatter.FormatNumber(m_explorer.Cost)}</color>";

        if (m_cooldown_coroutine == null)
        {
            m_button.interactable = true;
        }
    }

    private void Cooling()
    {
        if (m_cooldown_coroutine != null)
        {
            StopCoroutine(m_cooldown_coroutine);
        }

        m_cooldown_coroutine = StartCoroutine(Co_Cooling());
    }

    private IEnumerator Co_Cooling()
    {
        m_button.interactable = false;

        m_cooltime_image.gameObject.SetActive(true);
        m_cooltime_image.fillAmount = 1f;

        float elapsed_time = 0f;

        while (elapsed_time <= m_cooltime)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float delta = elapsed_time / m_cooltime;
            m_cooltime_image.fillAmount = 1f - delta;
        }

        m_cooltime_image.fillAmount = 0f;
        m_cooltime_image.gameObject.SetActive(false);
        m_button.interactable = true;

        m_cooldown_coroutine = null;
    }

    public void BUTTON_Call()
    {
        Cooling();

        m_factory.Instantiate(m_explorer.ID);

        m_cost_ctrl.UpdateCurrentCost(-m_explorer.Cost);
    }
}