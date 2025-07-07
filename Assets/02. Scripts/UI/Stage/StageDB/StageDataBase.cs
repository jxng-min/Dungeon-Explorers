using DeckService;
using UnityEngine;

[CreateAssetMenu(fileName = "New StageDataBase", menuName = "SO/DB/Create StageDataBase")]
public class StageDataBase : ScriptableObject
{
    #region Variables
    [Header("현재 플레이할 스테이지")]
    [SerializeField] private int m_stage;
    public int Stage
    {
        get => m_stage;
        set => m_stage = value;
    }

    [Header("덱 정보")]
    [SerializeField]
    private UnitCode[] m_deck = {
        UnitCode.EMPTY,
        UnitCode.EMPTY,
        UnitCode.EMPTY,
        UnitCode.EMPTY,
        UnitCode.EMPTY
    };

    private IDeckService deck_service;
    #endregion Variables

    #region Properties
    public UnitCode[] Deck
    {
        get => m_deck;
        set => m_deck = value;
    }
    #endregion Properties

    private void OnEnable()
    {
        deck_service = ServiceLocator.Instance.DeckService;
        m_deck = deck_service.GetDeck().ToArray();
    }
}