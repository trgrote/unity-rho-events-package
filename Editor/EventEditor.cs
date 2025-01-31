using UnityEditor;
using UnityEngine;

namespace rho
{
    [CustomEditor(typeof(Event))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            
            // Draw and trigger button if someone clicks it
            if(GUILayout.Button("Invoke"))
            {
                Event eventTarget = (Event) target;
                eventTarget.Invoke();
            }
            
            EditorGUI.EndDisabledGroup();
        }
    }
}