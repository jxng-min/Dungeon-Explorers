using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Reinforcer : MonoBehaviour
{
    [Header("강화 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;
    private ReinforcementSlot[] m_slots;

    private Animator m_animator;

    private void Awake()
    {
        m_slots = m_slot_root.GetComponentsInChildren<ReinforcementSlot>();
        m_animator = GetComponent<Animator>();
    }

    public void BUTTON_Open()
    {
        m_animator.SetBool("Open", true);
        UpdateSlots();
    }

    public void BUTTON_Close()
    {
        m_animator.SetBool("Open", false);
    }

    public void UpdateSlots()
    {
        foreach (var slot in m_slots)
        {
            slot.UpdateSlot();
        }
    }

    public int GetField(ReinforcementType type)
    {
        switch (type)
        {
            case ReinforcementType.TOWER_HP:
                return DataManager.Instance.Data.Reinforcement.TowerHP;

            case ReinforcementType.MAX_COST:
                return DataManager.Instance.Data.Reinforcement.MaxCost;

            case ReinforcementType.SKILL_DAMAGE:
                return DataManager.Instance.Data.Reinforcement.SkillDamage;

            case ReinforcementType.SKILL_COOLTIME:
                return DataManager.Instance.Data.Reinforcement.SkillCooltime;

            case ReinforcementType.SKILL_INTERVAL:
                return DataManager.Instance.Data.Reinforcement.SkillInterval;

            case ReinforcementType.GOLD:
                return DataManager.Instance.Data.Reinforcement.Gold;

            case ReinforcementType.EXP:
                return DataManager.Instance.Data.Reinforcement.EXP;
        }

        return -1;
    }

    public void UpgradeField(ReinforcementType type, int amount = 1)
    {
        switch (type)
        {
            case ReinforcementType.TOWER_HP:
                DataManager.Instance.Data.Reinforcement.TowerHP += amount;
                break;

            case ReinforcementType.MAX_COST:
                DataManager.Instance.Data.Reinforcement.MaxCost += amount;
                break;

            case ReinforcementType.SKILL_DAMAGE:
                DataManager.Instance.Data.Reinforcement.SkillDamage += amount;
                break;

            case ReinforcementType.SKILL_COOLTIME:
                DataManager.Instance.Data.Reinforcement.SkillCooltime += amount;
                break;

            case ReinforcementType.SKILL_INTERVAL:
                DataManager.Instance.Data.Reinforcement.SkillInterval += amount;
                break;

            case ReinforcementType.GOLD:
                DataManager.Instance.Data.Reinforcement.Gold += amount;
                break;

            case ReinforcementType.EXP:
                DataManager.Instance.Data.Reinforcement.EXP += amount;
                break;
        }
    }
}
