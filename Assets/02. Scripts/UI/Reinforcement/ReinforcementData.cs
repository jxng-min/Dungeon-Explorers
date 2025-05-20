[System.Serializable]
public class ReinforcementData
{
    public int TowerHP;
    public int MaxCost;
    public int SkillDamage;
    public int SkillCooltime;
    public int SkillInterval;
    public int Gold;
    public int EXP;

    public ReinforcementData()
    {
        TowerHP = 1;
        MaxCost = 1;
        SkillDamage = 1;
        SkillCooltime = 1;
        SkillInterval = 1;
        Gold = 1;
        EXP = 1;
    }
}