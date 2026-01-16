using UnityEditor;
using UnityEngine;

namespace rho
{
    [CustomEditor(typeof(GameObjectEvent))]
    public class GameObjectEventEditor : UnityObjectEventEditor<GameObject>
    {
    }
}