using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace rho
{
    /// <summary>
    /// Unity Wizard that will create a 'EventTYPE' folder and both Event types and Listener scripts.
    /// </summary>
    public class GenerateNewEventTypeWizard : ScriptableWizard
    {
        [MenuItem("Assets/Create/Rho/Create New Event<T> Type")]
        static void Init() => DisplayWizard<GenerateNewEventTypeWizard>("Create New Event<T>");

        #region ScriptableWizard Methods

        void OnWizardUpdate()
        {
            helpString = "Creates a new Event<T> of type";
            if (typeName == null || typeName.Length == 0)
            {
                errorString = "Please Provide Type";
                isValid = false;
            }
            else if(PathForEventAlreadyExists())
            {
                errorString = GetFullEventPathName() + " Already Exists";
                isValid = false;
            }
            else
            {
                errorString = "";
                isValid = true;
            }
        }

        void OnWizardCreate()
        {
            // Create new Folder
            AssetDatabase.CreateFolder(GetEventAssetPath(), GetEventClassName());

            // Create new Event Script, inside folder
            CreateNewScript(GetFullEventScriptName(), GetEventScriptContents());

            // Create new Event Listener Script, inside folder
            CreateNewScript(GetFullEventListenerScriptName(), GetEventListenerScriptContents());

            // Create Editor Script, if they want
            if (createEditorScript)
            {
                AssetDatabase.CreateFolder(GetFullEventPathName(), "Editor");
                CreateNewScript(GetFullEventEditorScriptName(), GetEditorScriptContents());
            }

            AssetDatabase.Refresh();
        }

        #endregion

        #region String Methods
        
        public string typeName;

        public bool createEditorScript;

        static string Capitalize(string str)
        {
            if (str.Length > 0)
            {
                str = char.ToUpper(str[0]) + str[1..];
            }
            return str;
        }

        private static string GetSelectedFolder()
        {
            var targetFolder = "Assets";
            var selectedAsset = Selection.GetFiltered<Object>(SelectionMode.Assets).FirstOrDefault();
            if (selectedAsset)
            {
                var assetPath = AssetDatabase.GetAssetPath(selectedAsset);
                if (AssetDatabase.IsValidFolder(assetPath))
                {
                    targetFolder = assetPath;
                }
                else
                {
                    targetFolder = Path.GetDirectoryName(assetPath);
                }
            }

            return targetFolder;
        }
        
        string GetEventAssetPath() => GetSelectedFolder();
        string GetEventFolderName() => $"Event{Capitalize(typeName)}";
        string GetFullEventPathName() => $"{GetEventAssetPath()}/{GetEventFolderName()}";

        string GetEventClassName() => $"Event{Capitalize(typeName)}";
        string GetEventListenerClassName() => $"EventListener{Capitalize(typeName)}";
        string GetEventEditorClassName() => $"Event{Capitalize(typeName)}Editor";

        string GetFullEventScriptName() => $"{GetFullEventPathName()}/{GetEventClassName()}.cs";
        string GetFullEventListenerScriptName() => $"{GetFullEventPathName()}/{GetEventListenerClassName()}.cs";
        string GetFullEventEditorScriptName() => $"{GetFullEventPathName()}/Editor/{GetEventEditorClassName()}.cs";
        
        bool PathForEventAlreadyExists() => AssetDatabase.IsValidFolder(GetFullEventPathName());

        #endregion

        #region Script Writing
        void CreateNewScript(string fullScriptName, string contents)
        {
            using StreamWriter sw = File.CreateText(fullScriptName);
            sw.WriteLine(contents);
        }

        string GetEventScriptContents()
        {
            return $@"using UnityEngine;

[CreateAssetMenu(fileName = ""{GetEventClassName()}"", menuName = ""Events/Event<{typeName}>"")]
public class {GetEventClassName()} : rho.Event<{typeName}>
{{
}}";
        }

        string GetEventListenerScriptContents()
        {
            return $@"using UnityEngine;

public class {GetEventListenerClassName()} : rho.EventListener<{typeName}>
{{
}}";
        }

        string GetEditorScriptContents()
        {
            return $@"using UnityEngine;
using UnityEditor;

[CustomEditor(typeof({GetEventClassName()}))]
public class {GetEventEditorClassName()} : rho.EventEditor<{typeName}>
{{
    {typeName} _testParam;

    protected override {typeName} GetTestParam()
    {{
        // TODO Implement something like this: return _testParam = EditorGUILayout.Toggle(""Test Parameter"", _testParam);
        throw new System.NotImplementedException();
    }}
}}";
        }
        #endregion
    }

}