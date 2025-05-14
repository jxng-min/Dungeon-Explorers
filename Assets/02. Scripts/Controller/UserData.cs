[System.Serializable]
public class UserData
{
    public int LV;
    public int EXP;
    public int Money;
    public int Stage;
    public InventoryItem[] Inventory;

    public UserData()
    {
        LV = 1;
        EXP = 0;
        Money = 0;
        Stage = 1;
        Inventory = new InventoryItem[] { new InventoryItem(0, 1) };
    }

    public UserData(int lv, int exp, int money, int stage, InventoryItem[] inventory)
    {
        LV = lv;
        EXP = exp;
        Money = money;
        Stage = stage;
        Inventory = inventory;
    }
}
