using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(EventFloat))]
    public class EventFloatEditor : EventEditor<float>
    {
        float _testParam;

        protected override float GetTestParam()
        {
            return _testParam = EditorGUILayout.FloatField("Test Parameter", _testParam);
        }
    }
}