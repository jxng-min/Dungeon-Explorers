namespace UserDataService
{
    [System.Serializable]
    public class UserData
    {
        public int LV;
        public int EXP;
        public int Stage;
        public UnitCode[] Deck;

        public UserData()
        {
            LV = 1;
            EXP = 0;
            Stage = 1;
            
            Deck = new UnitCode[] { UnitCode.NICK, UnitCode.EMPTY, UnitCode.EMPTY, UnitCode.EMPTY, UnitCode.EMPTY };

        }

        public UserData(int lv, int exp, int stage, UnitCode[] deck)
        {
            LV = lv;
            EXP = exp;
            Stage = stage;

            Deck = deck;
        }
    }
}