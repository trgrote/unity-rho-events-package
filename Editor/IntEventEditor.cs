using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(IntEvent))]
    public class IntEventEditor : EventEditor<int>
    {
        int _testParam;

        protected override int GetTestParam()
        {
            return _testParam = EditorGUILayout.IntField("Test Parameter", _testParam);
        }
    }
}