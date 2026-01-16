using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : EventEditor<float>
    {
        float _testParam;

        protected override float GetTestParam()
        {
            return _testParam = EditorGUILayout.FloatField("Test Parameter", _testParam);
        }
    }
}