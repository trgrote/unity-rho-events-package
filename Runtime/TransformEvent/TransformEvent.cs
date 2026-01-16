using UnityEngine;

namespace rho
{
    [CreateAssetMenu(fileName = "NewTransformEvent", menuName = "Rho/Events/Event<Transform>")]
    public class TransformEvent : Event<Transform>
    {
    }
}