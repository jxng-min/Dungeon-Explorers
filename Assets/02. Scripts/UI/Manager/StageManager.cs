using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    // [Header("스테이지 목록")]
    // [SerializeField] private Stage[] m_stages;

    private ExplorerInfo[] m_player_party;
    public ExplorerInfo[] Party
    {
        get => m_player_party;
        private set => m_player_party = value;
    }

    public void SetParty(int[] player_party)
    {
        List<ExplorerInfo> temp_party = new();

        foreach (var explorer_id in player_party)
        {
            if (explorer_id < 0)
            {
                temp_party.Add(new ExplorerInfo(-1, 0));
            }
            else
            {
                temp_party.Add(new ExplorerInfo(explorer_id, Inventory.Instance.GetItem(explorer_id).Upgrade));
            }
        }

        Party = temp_party.ToArray();
    }
}
