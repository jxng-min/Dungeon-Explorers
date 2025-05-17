using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    // [Header("스테이지 목록")]
    // [SerializeField] private Stage[] m_stages;

    private int[] m_player_party;
    public int[] Party
    {
        get => m_player_party;
        private set => m_player_party = value;
    }

    public void SetParty(int[] player_party)
    {
        Party = player_party;
    }

    
}
