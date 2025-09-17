using UnityEngine;

public class TitleBootstrapper : Bootstrapper
{
    protected override void Awake()
    {
        ServiceLocator.Initialize();
        base.Awake();
    }
}
