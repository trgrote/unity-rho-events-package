using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(EventBool))]
    public class EventBoolEditor : EventEditor<bool>
    {
        bool _testParam;

        protected override bool GetTestParam()
        {
            return _testParam = EditorGUILayout.Toggle("Test Parameter", _testParam);
        }
    }
}