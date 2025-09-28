using UnityEngine;

public class LoginBoostrapper : Bootstrapper
{
    protected override void Awake()
    {
        ServiceLocator.Initialize();
        base.Awake();
    }

    protected override void Start()
    {
        SoundManager.Instance.PlayBGM("Login BGM");
    }
}
