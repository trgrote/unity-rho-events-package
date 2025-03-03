using UnityEditor;

namespace rho
{
    public class EventUnityObjectEditor<T> : EventEditor<T> where T : UnityEngine.Object
    {
        T _testParam;

        protected override T GetTestParam()
        {
            return _testParam = EditorGUILayout.ObjectField("Test Parameter", _testParam, typeof(T), true) as T;
        }
    }
}