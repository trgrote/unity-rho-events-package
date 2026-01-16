using UnityEngine;
using UnityEngine.Events;

namespace rho
{
    public class EventListener<T> : MonoBehaviour
    {
        [SerializeField] Event<T> Event;
        [SerializeField] UnityEvent<T> Response;

        void OnEnable()
        {
            Event.Register(this);
        }

        void OnDisable()
        {
            Event.Unregister(this);
        }

        public void OnEventInvoked(T param)
        {
            Response.Invoke(param);
        }
    }
}