using UnityEngine;

public class TitleBootstrapper : Bootstrapper
{
    protected override void Start()
    {
        base.Start();
        SoundManager.Instance.PlayBGM("Title BGM");
    }
}
