public class GameManager : Singleton<GameManager>
{
    public GameEventType GameState { get; set; }

    private bool m_can_init;

    private new void Awake()
    {
        base.Awake();

        GameEventBus.Subscribe(GameEventType.LOGIN, Login);
        GameEventBus.Subscribe(GameEventType.LOADING, Loading);
        GameEventBus.Subscribe(GameEventType.WAITING, Waiting);
    }

    private void Start()
    {
        GameEventBus.Publish(GameEventType.LOGIN);
    }

    public void Login()
    {
        GameState = GameEventType.LOGIN;

        SoundManager.Instance.PlayBGM("Login");
    }

    public void Loading()
    {
        GameState = GameEventType.LOADING;
    }

    public void Waiting()
    {
        GameState = GameEventType.WAITING;

        SoundManager.Instance.PlayBGM("Title");
        m_can_init = true;
    }

    public void Playing()
    {
        GameState = GameEventType.PLAYING;

        if (m_can_init)
        {
            m_can_init = false;
            SoundManager.Instance.PlayBGM("Game");
        }
    }

    public void Pause()
    {
        GameState = GameEventType.PAUSE;
    }

    public void GameOver()
    {
        GameState = GameEventType.GAMEOVER;

        OpenResult();
    }

    public void GameClear()
    {
        GameState = GameEventType.GAMECLEAR;

        // if (DataManager.Instance.Data.Stage == StageManager.Instance.Current.ID)
        // {
        //     DataManager.Instance.Data.Stage++;
        // }
        OpenResult();
    }

    private void OpenResult()
    {
        // var result_ui = FindFirstObjectByType<ResultCtrl>();
        // if (result_ui == null)
        // {
        //     return;
        // }

        // result_ui.Open();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        // Inventory.Instance.SaveInventory();
        // DataManager.Instance.SaveJson();
    }
}
