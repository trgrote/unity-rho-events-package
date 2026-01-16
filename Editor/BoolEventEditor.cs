using UnityEditor;

namespace rho
{
    [CustomEditor(typeof(BoolEvent))]
    public class BoolEventEditor : EventEditor<bool>
    {
        bool _testParam;

        protected override bool GetTestParam()
        {
            return _testParam = EditorGUILayout.Toggle("Test Parameter", _testParam);
        }
    }
}