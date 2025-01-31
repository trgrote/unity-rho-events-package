using UnityEngine;
using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(EventInt))]
    public class EventIntEditor : EventEditor<int>
    {
        int _testParam;

        protected override int GetTestParam()
        {
            return _testParam = EditorGUILayout.IntField("Test Parameter", _testParam);
        }
    }
}