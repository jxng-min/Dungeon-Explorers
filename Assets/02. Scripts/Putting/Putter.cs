using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Putter : MonoBehaviour
{
    [Header("선발대 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_elected_slot_root;
    private PuttingSlot[] m_elected_slots;
    public PuttingSlot[] ElectedSlots { get => m_elected_slots; }

    [Header("후발대 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_candidate_slot_root;
    private List<PuttingSlot> m_candidate_slots;
    public List<PuttingSlot> CandidateSlots { get => m_candidate_slots; }

    private Animator m_putter_animator;

    private void Awake()
    {
        m_elected_slots = m_elected_slot_root.GetComponentsInChildren<PuttingSlot>();
        m_candidate_slots = new();
        m_putter_animator = GetComponent<Animator>();
    }

    public void BUTTON_Open()
    {
        m_putter_animator.SetBool("Open", true);

        Initialize();
    }

    public void BUTTON_Close()
    {
        m_putter_animator.SetBool("Open", false);

        SaveElectedData();
        ReturnCandidateSlots();
    }

    private void Initialize()
    {
        LoadElectedData();
        LoadCandidateSlots();
    }

    private void LoadElectedData()
    {
        var party = DataManager.Instance.Data.Party;
        for(int i = 0; i < party.Length; i++)
        {
            if(party[i] < 0)
            {
                m_elected_slots[i].Clear();
            }
            else
            {
                m_elected_slots[i].Add(ExplorerDataManager.Instance.GetExplorer(party[i]));
            }
        }
    }

    private void SaveElectedData()
    {
        List<int> party = new();
        foreach(var slot in m_elected_slots)
        {
            if(slot.Explorer == null)
            {
                party.Add(-1);
            }
            else
            {
                party.Add(slot.Explorer.ID);
            }
        }

        DataManager.Instance.Data.Party = party.ToArray();
    }

    private void LoadCandidateSlots()
    {
        foreach(var item in Inventory.Instance.List)
        {
            var slot = ObjectManager.Instance.GetObject(ObjectType.PUTTING_SLOT).GetComponent<PuttingSlot>();
            slot.transform.SetParent(m_candidate_slot_root);
            m_candidate_slots.Add(slot);

            var explorer = ExplorerDataManager.Instance.GetExplorer(item.ID);
            slot.Add(explorer);
            if(IsAlreadyElected(explorer, out int index))
            {
                slot.Equipped();
            }
        }
    }

    private void ReturnCandidateSlots()
    {
        foreach(var item in m_candidate_slots)
        {
            ObjectManager.Instance.ReturnObject(item.gameObject, ObjectType.PUTTING_SLOT);
        }

        m_candidate_slots.Clear();
    }

    public bool IsAlreadyElected(Explorer explorer, out int index)
    {
        index = -1;

        for(int i = 0; i < m_elected_slots.Length; i++)
        {
            if(m_elected_slots[i].Explorer != null && m_elected_slots[i].Explorer.ID == explorer.ID)
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    public void Swap(Explorer from, PuttingSlot to)
    {
        GetCandidateSlot(from).Equipped();
        to.Add(from);
    }

    public void SetElectedSlotsGlow(bool set)
    {
        foreach(var slot in m_elected_slots)
        {
            slot.Animator.SetBool("Glow", set);
        }
    }

    public PuttingSlot GetCandidateSlot(Explorer explorer)
    {
        foreach(var slot in m_candidate_slots)
        {
            if(slot.Explorer.ID == explorer.ID)
            {
                return slot;
            }
        }

        return null;
    }

    public PuttingSlot GetElectedSlot(Explorer explorer)
    {
        foreach(var slot in m_elected_slots)
        {
            if(slot.Explorer != null && slot.Explorer.ID == explorer.ID)
            {
                return slot;
            }
        }
        
        return null;
    }
}
