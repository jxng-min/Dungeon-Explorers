using UnitRepository;
using EXPService;
using InventoryService;
using UserDataService;
using ReinforcementService;
using DeckService;
using SettingService;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, object> m_services = new();

    public static IDictionary<Type, object> Services { get => m_services; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Register<IUnitRepository>(new LocalUnitRepository());
        Register<IEXPService>(new LocalEXPSystem());
        Register<IInventoryService>(new LocalInventory());
        Register<IUserDataService>(new LocalUserDataSystem());
        Register<IReinforcementService>(new LocalReinforcementSystem());
        Register<IDeckService>(new LocalDeckSystem());
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