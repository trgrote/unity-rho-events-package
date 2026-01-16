using UnityEngine;
using UnityEngine.Events;

namespace rho
{
    public class EventListener : MonoBehaviour
    {
        public Event Event;
        public UnityEvent Response;

        void OnEnable()
        {
            Event.Register(this);
        }

        void OnDisable()
        {
            Event.Unregister(this);
        }

        public void OnEventInvoked()
        {
            Response.Invoke();
        }
    }
}