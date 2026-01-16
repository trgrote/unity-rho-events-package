using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(TransformEvent))]
    public class TransformEventEditor : UnityObjectEventEditor<Transform>
    {
    }
}