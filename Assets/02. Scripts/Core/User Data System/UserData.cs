namespace UserDataService
{
    [System.Serializable]
    public class UserData
    {
        public int LV;
        public int EXP;
        public int Stage;

        public UserData()
        {
            LV = 1;
            EXP = 0;
            Stage = 1;
        }

        public UserData(int lv, int exp, int stage, UnitCode[] deck)
        {
            LV = lv;
            EXP = exp;
            Stage = stage;
        }
    }
}