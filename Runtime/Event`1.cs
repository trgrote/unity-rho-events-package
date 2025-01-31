using System;
using System.Collections.Generic;
using UnityEngine;

namespace rho
{
    public class Event<T> : ScriptableObject
    {
        List<EventListener<T>> _listeners = new List<EventListener<T>>();

        public void Invoke(T param)
        {
            for (var i = _listeners.Count - 1; i >= 0 ; --i)
            {
                _listeners[i].OnEventInvoked(param);
            }
        }

        public void Register(EventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(EventListener<T> listener)
        {
            _listeners.Remove(listener);
        }    
    }
}