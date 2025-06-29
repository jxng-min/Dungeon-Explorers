using System.Collections.Generic;
using DeckService;
using Units;
using UnityEngine;

public class DeckSlotModel
{
    #region Variables
    private IDeckService m_deck_system;
    private UnitDataBase m_unit_db;
    private UnitCode m_unit_code;
    private ISelectorView m_selector_view;
    private IDeckView m_deck_view;
    #endregion Variables

    #region Properties
    public UnitCode Code
    {
        get => m_unit_code;
        set => m_unit_code = value;
    }
    public List<UnitCode> Deck { get => m_deck_system.GetDeck(); }
    public IDeckService DeckSystem { get => m_deck_system; }
    public Unit Unit { get => m_unit_db.GetUnit(m_unit_code); }
    public int Cost { get => (m_unit_db.GetUnit(m_unit_code) as Hero).Cost; }
    public Sprite Image { get => m_unit_db.GetUnit(m_unit_code).Image; }
    public ISelectorView SelectorView { get => m_selector_view; }
    public IDeckView DeckView { get => m_deck_view; }
    #endregion Properties

    public DeckSlotModel()
    {
        m_unit_code = UnitCode.EMPTY;
    }

    public void Initialize(UnitDataBase unit_db, IDeckService deck_system, IDeckView deck_view, ISelectorView selector_view, UnitCode code)
    {
        m_unit_db = unit_db;
        m_deck_system = deck_system;
        m_deck_view = deck_view;
        m_unit_code = code;
        m_selector_view = selector_view;
    }
}
