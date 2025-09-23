public enum ObjectType
{
    NONE = -1,
    
    #region SFX / UI (0 ~ 99)
    SFX = 0, DECK_SLOT = 1, TRANING_SLOT = 2, DAMAGE_INDICATOR = 3,
    #endregion SFX / UI

    #region Unit (1000 ~ 1999)
    MELEE_UNIT = 1000, RANGED_UNIT = 1001, NIMMIA = 1002, LELIA = 1003,
    #endregion Unit

    #region Skill (2000 ~ 2999)
    METEOR = 2000, ARROW = 2001, HOLY_SHIELD = 2002, HOLY_CROSS = 2003,  
    #endregion Skill
}