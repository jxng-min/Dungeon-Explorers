using DeckService;
using InventoryService;
using ObjectPool;
using ReinforcerService;
using UnityEngine;
using UserService;

public class GameManager : Singleton<GameManager>
{
    public GameEventType GameState { get; set; }

    private bool m_can_init;

    public override void Awake()
    {
        base.Awake();
        ServiceLocator.Initialize();
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.LOGIN, Login);
        GameEventBus.Subscribe(GameEventType.LOADING, Loading);
        GameEventBus.Subscribe(GameEventType.WAITING, Waiting);

        GameEventBus.Publish(GameEventType.LOGIN);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.LOGIN, Login);
        GameEventBus.Unsubscribe(GameEventType.LOADING, Loading);
        GameEventBus.Unsubscribe(GameEventType.WAITING, Waiting);
    }

    public void Login()
    {
        GameState = GameEventType.LOGIN;

        SoundManager.Instance.PlayBGM("Login");
    }

    public void Loading()
    {
        GameState = GameEventType.LOADING;

        ObjectManager.Instance.ReturnObjectsAll();
        Time.timeScale = 1f;
    }

    public void Waiting()
    {
        GameState = GameEventType.WAITING;

        SoundManager.Instance.PlayBGM("Title");
        m_can_init = true;

        Time.timeScale = 1f;
    }

    public void Playing()
    {
        GameState = GameEventType.PLAYING;

        if (m_can_init)
        {
            m_can_init = false;
            SoundManager.Instance.PlayBGM("Game");
        }

        Time.timeScale = 1f;
    }

    public void Pause()
    {
        GameState = GameEventType.PAUSE;

        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        GameState = GameEventType.GAMEOVER;

        OpenResult();
        SaveData();

        Time.timeScale = 0f;
    }

    public void GameClear()
    {
        GameState = GameEventType.GAMECLEAR;

        OpenResult();
        SaveData();

        Time.timeScale = 0f;
    }

    private void OpenResult()
    {
        var result_ui = FindFirstObjectByType<ResultViewer>();
        if (result_ui == null)
        {
            return;
        }

        result_ui.OpenUI();
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
        ServiceLocator.Get<IInventoryService>().Save();
        ServiceLocator.Get<IReinforcerService>().Save();
        ServiceLocator.Get<IDeckService>().Save();
        ServiceLocator.Get<ISettingService>().Save();
        ServiceLocator.Get<IUserService>().Save();
    }
}
