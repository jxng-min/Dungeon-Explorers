using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{
    private List<InventoryItem> m_item_list;
    public List<InventoryItem> List
    {
        get { return m_item_list; }
    }

    private new void Awake()
    {
        base.Awake();

        m_item_list = new();
    }

    private void Start()
    {
        LoadInventory();
    }

    public bool CheckHasItem(int explorer_id)
    {
        foreach(var item in m_item_list)
        {
            if(item.ID == explorer_id)
            {
                return true;
            }
        }

        return false;
    }

    public void TryAdd(int explorer_id, int upgrade = 1)
    {
        if(CheckHasItem(explorer_id))
        {
            return;
        }

        m_item_list.Add(new InventoryItem(explorer_id, upgrade));
    }

    public InventoryItem GetItem(int explorer_id)
    {
        foreach(var item in m_item_list)
        {
            if(item.ID == explorer_id)
            {
                return item;
            }
        }

        return null;
    }

    private void LoadInventory()
    {
        foreach(var item in DataManager.Instance.Data.Inventory)
        {
            m_item_list.Add(item);
        }
    }

    public void SaveInventory()
    {
        DataManager.Instance.Data.Inventory = m_item_list.ToArray();
    }
}