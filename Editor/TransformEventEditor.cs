using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(TransformEvent))]
    public class TransformEventEditor : EventEditor<Transform>
    {
        Transform _testParam;

        protected override Transform GetTestParam()
        {
            return _testParam = EditorGUILayout.ObjectField("Test Parameter", _testParam, typeof(Transform), true) as Transform;
        }
    }
}