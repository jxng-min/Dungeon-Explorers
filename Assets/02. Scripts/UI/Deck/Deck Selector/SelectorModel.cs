using DeckService;
using Units;

public enum SelectorWorkingMode
{
    NONE,
    EQUIPPING,
    DISSOLVING,
}

public class SelectorModel
{
    #region Variables
    private Unit m_unit;
    private IDeckView m_deck_view;
    private IDeckSlotView m_deck_slot;
    private SelectorWorkingMode m_mode;
    #endregion Variables

    public SelectorModel(IDeckView deck_view)
    {
        m_deck_view = deck_view;
    }

    #region Properties
    public Unit Unit
    {
        get => m_unit;
        set => m_unit = value;
    }

    public IDeckView DeckView
    {
        get => m_deck_view;
    }

    public IDeckSlotView DeckSlot
    {
        get => m_deck_slot;
        set => m_deck_slot = value;
    }

    public SelectorWorkingMode Mode
    {
        get => m_mode;
        set => m_mode = value;
    }
    #endregion Properties
}
