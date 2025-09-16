using System;
using System.Collections.Generic;

public static class DIContainer
{
    private static Dictionary<Type, object> m_instances = new();

    public static void Register<T>(T instance)
    {
        m_instances[typeof(T)] = instance;
    }

    public static bool IsRegistered<T>()
    {
        return m_instances.ContainsKey(typeof(T));
    }

    public static T Resolve<T>()
    {
        return (T)m_instances[typeof(T)];
    }

    public static void Clear()
    {
        m_instances.Clear();
    }
}
