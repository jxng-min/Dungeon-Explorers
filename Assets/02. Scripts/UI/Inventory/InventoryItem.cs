[System.Serializable]
public class InventoryItem
{
    public int ID;
    public int Upgrade;

    public InventoryItem(int id, int upgrade)
    {
        ID = id;
        Upgrade = upgrade;
    }
}
