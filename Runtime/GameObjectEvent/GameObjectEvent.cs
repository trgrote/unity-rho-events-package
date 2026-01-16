using UnityEngine;

namespace rho
{
    [CreateAssetMenu(fileName = "NewGameObjectEvent", menuName = "Rho/Events/Event<GameObject>")]
    public class GameObjectEvent : Event<GameObject>
    {
    }
}