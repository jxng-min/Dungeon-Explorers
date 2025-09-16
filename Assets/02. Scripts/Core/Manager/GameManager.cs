using DeckService;
using InventoryService;
using ObjectPool;
using ReinforcementService;
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
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.LOGIN, Login);
        GameEventBus.Unsubscribe(GameEventType.LOADING, Loading);
        GameEventBus.Unsubscribe(GameEventType.WAITING, Waiting);
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

        ObjectManager.Instance.ReturnObjectsAll();
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
        else
        {
            ToggleUnits(true);
            ToggleSkills(true);
        }
    }

    public void Pause()
    {
        GameState = GameEventType.PAUSE;

        ToggleUnits(false);
        ToggleSkills(false);
    }

    public void GameOver()
    {
        GameState = GameEventType.GAMEOVER;

        OpenResult();
        SaveData();
    }

    public void GameClear()
    {
        GameState = GameEventType.GAMECLEAR;

        OpenResult();
        SaveData();
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
        ServiceLocator.Get<IReinforcementService>().Save();
        ServiceLocator.Get<IDeckService>().Save();
        ServiceLocator.Get<ISettingService>().Save();
        ServiceLocator.Get<IUserService>().Save();
    }

    private void ToggleUnits(bool is_play)
    {
        var unit_list = ObjectManager.Instance.ActiveUnitObjects;
        foreach (var unit_obj in unit_list)
        {
            var unit = unit_obj.GetComponent<BaseUnit>();
            unit.Animator.speed = is_play ? 1f : 0f;
        }
    }

    private void ToggleSkills(bool is_play)
    {
        var skill_list = ObjectManager.Instance.ActiveSkillObjects;
        foreach (var skill_obj in skill_list)
        {
            var skill = skill_obj.GetComponent<Skill>();

            if (is_play)
                skill.Resume();
            else
                skill.Stop();
        }
    }
}
