using UnityEditor;
using UnityEngine;

namespace rho
{
    public abstract class EventEditor<T> : Editor
    {
        protected abstract T GetTestParam();

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            var testParam = GetTestParam();
            
            // Draw and trigger button if someone clicks it
            if(GUILayout.Button("Invoke"))
            {
                Event<T> eventTarget = (Event<T>) target;
                eventTarget.Invoke(testParam);
            }
            
            EditorGUI.EndDisabledGroup();
        }
    }
}