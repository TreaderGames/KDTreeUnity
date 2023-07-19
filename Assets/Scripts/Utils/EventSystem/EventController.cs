using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback(System.Object arg);

public class EventController : MonoBehaviour
{
    private static Dictionary<EventID, List<Delegate>> eventTable = new Dictionary<EventID, List<Delegate>>();

    #region Public
    public static void StartListening(EventID eventType, Callback handler, int priority = 5)
    {
        // Obtain a lock on the event table to keep this thread-safe.
        lock (eventTable)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, new List<Delegate>());
            }

            List<Delegate> value = eventTable[eventType];

            value.Add(handler);
        }
    }

    public static void StopListening(EventID eventType, Callback handler)
    {
        // Obtain a lock on the event table to keep this thread-safe.
        lock (eventTable)
        {
            if (eventTable.ContainsKey(eventType))
            {
                List<Delegate> value = eventTable[eventType];
                value.Remove(handler);
            }
        }
    }

    public static void TriggerEvent(EventID eventType, System.Object arg = null)
    {
        if (eventTable.ContainsKey(eventType))
        {
            List<Delegate> value = eventTable[eventType];

            foreach (Delegate observer in value)
            {
                observer.DynamicInvoke(arg);
            }
        }
    }

    #endregion
}
