using UnityEditor;
using UnityEngine;

namespace rho
{
    [CustomEditor(typeof(EventGameObject))]
    public class EventGameObjectEditor : EventEditor<GameObject>
    {
        GameObject _testParam;

        protected override GameObject GetTestParam()
        {
            return _testParam = EditorGUILayout.ObjectField("Test Parameter", _testParam, typeof(GameObject), true) as GameObject;
        }
    }
}