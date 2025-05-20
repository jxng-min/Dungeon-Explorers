using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("스테이지 목록")]
    [SerializeField] private List<Stage> m_stages;
    private Dictionary<int, Stage> m_stage_dict;

    private ExplorerInfo[] m_player_party;
    public ExplorerInfo[] Party
    {
        get => m_player_party;
        private set => m_player_party = value;
    }

    private Stage m_current_stage;
    public Stage Current
    {
        get => m_current_stage;
        set => m_current_stage = value;
    }

    private new void Awake()
    {
        base.Awake();

        m_stage_dict = new();

        foreach (var stage in m_stages)
        {
            m_stage_dict.Add(stage.ID, stage);
        }
    }

    public Stage GetStage(int id)
    {
        return m_stage_dict.ContainsKey(id) ? m_stage_dict[id] : null;
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
