using UnityEngine;

public class SpawnPresenter
{
    private readonly ISpawnView m_view;
    private readonly IStageDataBase m_stage_db;

    private Stage m_stage;

    public float Timer { get; set; }
    public int Current { get; set; }
    public int Last => m_stage.Waves.Length;
    public Wave Wave => m_stage.Waves[Current];


    public SpawnPresenter(ISpawnView view,
                          IStageDataBase stage_db)
    {
        m_view = view;
        m_stage_db = stage_db;

        m_stage = m_stage_db.GetStage(m_stage_db.Current);

        m_view.Inject(this);
        m_view.StartWave();
    }
}
