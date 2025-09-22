using EXPService;
using InventoryService;
using ReinforcerService;
using DeckService;
using SettingService;
using System.Collections.Generic;
using System;
using UnitService;
using UserService;

public static class ServiceLocator
{
    private static Dictionary<Type, object> m_services = new();

    public static IDictionary<Type, object> Services { get => m_services; }

    public static void Initialize()
    {
        Register<IUnitService>(new LocalUnitService());
        Register<IEXPService>(new LocalEXPService());
        Register<IInventoryService>(new LocalInventoryService());
        Register<IUserService>(new LocalUserService());
        Register<IReinforcerService>(new LocalReinforcerService());
        Register<IDeckService>(new LocalDeckService());
        Register<ISettingService>(new LocalSettingSystem());
    }

    public static void Register<T>(T service)
    {
        if (!m_services.ContainsKey(typeof(T)))
        {
            m_services.Add(typeof(T), service);
        }
    }

    public static T Get<T>()
    {
        return (T)m_services[typeof(T)];
    }
}