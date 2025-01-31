using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(EventVector3))]
    public class EventVector3Editor : EventEditor<Vector3>
    {
        Vector3 _testParam;

        protected override Vector3 GetTestParam()
        {
            return _testParam = EditorGUILayout.Vector3Field("Test Parameter", _testParam);
        }
    }
}