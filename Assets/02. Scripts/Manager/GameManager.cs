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

        ControlAnimation(true);
        ObjectManager.Instance.ReturnRangeObject(ObjectType.METEOR, ObjectType.DAMAGE_INDICATOR);
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
            ControlAnimation(true);
        }
    }

    public void Pause()
    {
        GameState = GameEventType.PAUSE;
        ControlAnimation(false);
    }

    public void GameOver()
    {
        GameState = GameEventType.GAMEOVER;

        ControlAnimation(false);
        OpenResult();
    }

    public void GameClear()
    {
        GameState = GameEventType.GAMECLEAR;

        ControlAnimation(false);

        if (DataManager.Instance.Data.Stage == StageManager.Instance.Current.ID)
        {
            DataManager.Instance.Data.Stage++;
        }
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

    public void ControlAnimation(bool is_play)
    {
        var explorers = ObjectManager.Instance.GetActiveObjects(ObjectType.EXPLORER);
        foreach (var explorer in explorers)
        {
            var character = explorer.GetComponent<Character>();
            character.Animator.speed = is_play ? 1f : 0f;
        }

        var enemys = ObjectManager.Instance.GetActiveObjects(ObjectType.ENEMY);
        foreach (var enemy in enemys)
        {
            var enemy_ctrl = enemy.GetComponent<EnemyCtrl>();
            enemy_ctrl.Animator.speed = is_play ? 1f : 0f;
        }

        var arrow_objs = ObjectManager.Instance.GetActiveObjects(ObjectType.ARROW);
        foreach (var arrow_obj in arrow_objs)
        {
            var arrow = arrow_obj.GetComponent<Arrow>();
            if (is_play)
            {
                arrow.Resume();
            }
            else
            {
                arrow.Stop();
            }
        }

        var shield_objs = ObjectManager.Instance.GetActiveObjects(ObjectType.HOLY_SHIELD);
        foreach (var shield_obj in shield_objs)
        {
            var shield = shield_obj.GetComponent<HolyShield>();
            if (is_play)
            {
                shield.Resume();
            }
            else
            {
                shield.Stop();
            }
        }

        var cross_objs = ObjectManager.Instance.GetActiveObjects(ObjectType.HOLY_CROSS);
        foreach (var cross_obj in cross_objs)
        {
            var cross = cross_obj.GetComponent<HolyCross>();
            if (is_play)
            {
                cross.Resume();
            }
            else
            {
                cross.Stop();
            }
        }
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
