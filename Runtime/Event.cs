using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rho
{
    [CreateAssetMenu(menuName = "Rho/Events/Event")]
    public class Event : ScriptableObject
    {
        List<EventListener> _listeners = new List<EventListener>();

        public void Invoke()
        {
            for (var i = _listeners.Count - 1; i >= 0 ; --i)
            {
                _listeners[i].OnEventInvoked();
            }
        }

        public void Register(EventListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(EventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}