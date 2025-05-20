public class GameManager : Singleton<GameManager>
{
    public GameEventType GameState { get; set; }

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
    }

    public void Loading()
    {
        GameState = GameEventType.LOADING;

        ObjectManager.Instance.ReturnRangeObject(ObjectType.METEOR, ObjectType.DAMAGE_INDICATOR);
    }

    public void Waiting()
    {
        GameState = GameEventType.WAITING;
    }

    public void Playing()
    {
        GameState = GameEventType.PLAYING;
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

        OpenResult();
    }

    private void OpenResult()
    {
        var result_ui = FindFirstObjectByType<ResultCtrl>();
        if (result_ui == null)
        {
            return;
        }

        result_ui.Open();
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
        Inventory.Instance.SaveInventory();
        DataManager.Instance.SaveJson();
    }
}
