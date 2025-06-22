using InventoryService;

namespace UserDataService
{
    [System.Serializable]
    public class UserData
    {
        public int LV;
        public int EXP;
        public int Stage;
        public int Money;
        public Unit[] Inventory;
        public UnitCode[] Deck;
        public ReinforcementData Reinforcement;

        public UserData()
        {
            LV = 1;
            EXP = 0;
            Stage = 1;
            
            Money = 0;
            Inventory = new Unit[] { new Unit(0, 1) };
            Deck = new UnitCode[] { 0, -1, -1, -1, -1 };

            Reinforcement = new ReinforcementData();
        }

        public UserData(int lv, int exp, int money, int stage, Unit[] inventory, UnitCode[] deck, ReinforcementData reinforcement)
        {
            LV = lv;
            EXP = exp;
            Stage = stage;

            Money = money;
            Inventory = inventory;
            Deck = deck;
            
            Reinforcement = reinforcement;
        }
    }
}