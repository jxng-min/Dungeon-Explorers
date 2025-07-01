public enum ObjectType
{
    #region SFX / UI (0 ~ 99)
    SFX = 0, DECK_SLOT = 1, TRANING_SLOT = 2, DAMAGE_INDICATOR = 3,
    #endregion SFX / UI

    #region Unit (1000 ~ 1999)
    MELEE_UNIT = 1000, RANGED_UNIT = 1001, MAGIC_UNIT = 1002,
    #endregion Unit

    #region Skill (2000 ~ 2999)
    METEOR = 2000, ARROW = 2001, EXPLOSION = 2002, HOLY_SHIELD = 2003, HOLY_CROSS = 2004,  
    #endregion Skill
}