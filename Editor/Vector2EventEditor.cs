using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(Vector2Event))]
    public class Vector2EventEditor : EventEditor<Vector2>
    {
        Vector2 _testParam;

        protected override Vector2 GetTestParam()
        {
            return _testParam = EditorGUILayout.Vector2Field("Test Parameter", _testParam);
        }
    }
}