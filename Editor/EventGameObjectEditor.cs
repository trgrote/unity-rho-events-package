using UnityEditor;
using UnityEngine;

namespace rho
{
    [CustomEditor(typeof(EventGameObject))]
    public class EventGameObjectEditor : EventUnityObjectEditor<GameObject>
    {
    }
}