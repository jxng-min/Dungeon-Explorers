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
    private bool m_is_candidate;
    #endregion Variables

    #region Properties
    public List<UnitCode> Deck { get => m_deck_system.GetDeck(); }
    public IDeckService DeckSystem { get => m_deck_system; }
    public Unit Unit { get => m_unit_db.GetUnit(m_unit_code); }
    public int Cost { get => (m_unit_db.GetUnit(m_unit_code) as Hero).Cost; }
    public Sprite Image { get => m_unit_db.GetUnit(m_unit_code).Image; }
    public ISelectorView SelectorView { get => m_selector_view; }
    public bool IsCandidate { get => m_is_candidate; }
    #endregion Properties

    public void Initialize(UnitDataBase unit_db, IDeckService deck_system, ISelectorView selector_view, UnitCode code, bool is_candidate)
    {
        m_unit_db = unit_db;
        m_deck_system = deck_system;
        m_unit_code = code;
        m_selector_view = selector_view;
        m_is_candidate = is_candidate;
    }
}
