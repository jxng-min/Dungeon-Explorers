using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventBus : MonoBehaviour
{
    private static readonly IDictionary<GameEventType, UnityEvent> m_events = new Dictionary<GameEventType, UnityEvent>();

    public static void Subscribe(GameEventType event_type, UnityAction listner)
    {
        UnityEvent this_event;

        if (m_events.TryGetValue(event_type, out this_event))
        {
            this_event.AddListener(listner);
        }
        else
        {
            this_event = new UnityEvent();
            this_event.AddListener(listner);

            m_events.Add(event_type, this_event);
        }

        Debug.Log($"{event_type}이 정상적으로 구독되었습니다.");
    }

    public static void Unsubscribe(GameEventType event_type, UnityAction listener)
    {
        UnityEvent this_event;

        if (m_events.TryGetValue(event_type, out this_event))
        {
            this_event.RemoveListener(listener);
            Debug.Log($"{event_type}이 정상적으로 구독 취소되었습니다.");
        }
    }
    
    public static void Publish(GameEventType event_type)
    {
        UnityEvent this_event;

        if (m_events.TryGetValue(event_type, out this_event))
        {
            this_event.Invoke();
            Debug.Log($"{event_type}이 정상적으로 실행되었습니다.");
        }
    }
}
