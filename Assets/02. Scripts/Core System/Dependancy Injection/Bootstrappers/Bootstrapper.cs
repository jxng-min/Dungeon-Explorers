using UnityEngine;

public abstract class Bootstrapper : MonoBehaviour
{
    private IInstaller[] m_installers;

    protected virtual void Awake()
    {
        m_installers = transform.GetComponentsInChildren<IInstaller>();
    }

    protected virtual void Start()
    {
        foreach(var installer in m_installers)
        {
            installer.Install();
        }
    }
} 