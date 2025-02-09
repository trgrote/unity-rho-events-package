using System;
using System.IO;
using System.Linq;
using UnityEditor;

namespace rho
{
    /// <summary>
    /// Unity Wizard that will create a 'EventTYPE' folder and both Event types and Listener scripts.
    /// </summary>
    public class GenerateNewEventTypeWizard : ScriptableWizard
    {
        [MenuItem("Assets/Create/Rho/Create New Event<T> Type")]
        static void Init() => DisplayWizard<GenerateNewEventTypeWizard>("Create New Event<T>");

        #region SerializedFields
        
        public string _typeName;

        public bool _createEditorScript;

        #endregion

        #region ScriptableWizard Methods

        void OnWizardUpdate()
        {
            helpString = "Creates a new Event<T> of type";
            errorString = "";
            isValid = true;

            if (_typeName == null || _typeName.Length == 0)
            {
                errorString = "Please Provide Type";
                isValid = false;
                return;
            }
            
            // Try to find type
            var types = TypeSearcher.SearchForType(_typeName);

            if (types.Count() == 1)
            {
                var type = types.First();
                helpString = $"Creates a new Event<{type.FullName}> type";
            }
            else if (types.Count() > 1)
            {
                errorString = $"Found {types.Count()} Types: ";
                if (types.Count() < 10)
                {
                    errorString += string.Join(',', types.Select(t => t.FullName));
                }
                isValid = false;
            }
        }

        void OnWizardCreate()
        {
            var types = TypeSearcher.SearchForType(_typeName);

            if (types.Count() == 1)
            {
                var type = types.First();
                CreateEventScripts(type, _createEditorScript);
            }
            else
            {
                UnityEngine.Debug.LogError("Attempting to create event scripts of unknown type");
            }
        }

        void CreateEventScripts(Type type, bool createEditorScript)
        {
            var fullTypeName = type.FullName;
            var prettyTypeName = GetPrettyTypeName(type);
            var eventClassName = $"Event{prettyTypeName}";

            // Create new Folder
            var folderGUID = AssetDatabase.CreateFolder(GetSelectedAssetFolder(), eventClassName);
            var folderName = AssetDatabase.GUIDToAssetPath(folderGUID);

            // Create new Event Script, inside folder
            var eventScriptName = $"{folderName}/{eventClassName}.cs";
            CreateNewScript(eventScriptName, GetEventScriptContents(
                eventClassName: eventClassName, 
                fullTypeName: fullTypeName
            ));

            // Create new Event Listener Script, inside folder
            var eventListenerClassName = $"{eventClassName}Listener";
            var eventListenerScriptName = $"{folderName}/{eventListenerClassName}.cs";
            CreateNewScript(eventListenerScriptName, GetEventListenerScriptContents(
                listenerClassName: eventListenerClassName, 
                fullTypeName: fullTypeName
            ));

            // Create Editor Script, if they want
            if (createEditorScript)
            {
                var editorFolderGUID = AssetDatabase.CreateFolder(folderName, "Editor");
                var editorFolderName = AssetDatabase.GUIDToAssetPath(editorFolderGUID);
                
                var eventEditorClassName = $"{eventClassName}Editor";
                var eventEditorScriptName = $"{editorFolderName}/{eventEditorClassName}.cs";

                CreateNewScript(eventEditorScriptName, GetEditorScriptContents(
                    editorClassName: eventEditorClassName, 
                    eventClassName: eventClassName,
                    fullTypeName: fullTypeName,
                    type: type
                ));
            }

            AssetDatabase.Refresh();
        }

        private string GetPrettyTypeName(Type type)
        {
            // Check if the type is a built in type
            if (TypeSearcher.TryGetBuiltInType(type, out var builtInName))
            {
                // if so, use the built in name
                return Capitalize(builtInName);
            }

            return type.Name;
        }

        #endregion

        #region String Methods

        static string Capitalize(string str)
        {
            if (str.Length > 0)
            {
                str = char.ToUpper(str[0]) + str[1..];
            }
            return str;
        }

        private static string GetSelectedAssetFolder()
        {
            var targetFolder = "Assets";
            var selectedAsset = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets).FirstOrDefault();
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

        #endregion

        #region Script Writing

        void CreateNewScript(string fullScriptName, string contents)
        {
            using StreamWriter sw = File.CreateText(fullScriptName);
            sw.WriteLine(contents);
        }

        string GetEventScriptContents(string eventClassName, string fullTypeName)
        {
            return $@"[UnityEngine.CreateAssetMenu(fileName = ""{eventClassName}"", menuName = ""Events/Event<{fullTypeName}>"")]
public class {eventClassName} : rho.Event<{fullTypeName}>
{{
}}";
        }

        string GetEventListenerScriptContents(string listenerClassName, string fullTypeName)
        {
            return $@"public class {listenerClassName} : rho.EventListener<{fullTypeName}>
{{
}}";
        }

        string GetEditorScriptContents(string editorClassName, string eventClassName, string fullTypeName, Type type)
        {
            var baseClassName = "EventEditor";
            var classBody = $@"{{
    {fullTypeName} _testParam;

    protected override {fullTypeName} GetTestParam()
    {{
        // TODO Implement something like this: return _testParam = EditorGUILayout.Toggle(""Test Parameter"", _testParam);
        throw new System.NotImplementedException();
    }}
}}";

            if (type.IsSubclassOf(typeof(System.Enum)))
            {
                baseClassName = "EventEnumEditor";
                classBody = "{\n}";
            }
            else if (type.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                baseClassName = "EventUnityObjectEditor";
                classBody = "{\n}";
            }

            return $@"[UnityEditor.CustomEditor(typeof({eventClassName}))]
public class {editorClassName} : rho.{baseClassName}<{fullTypeName}>
{classBody}";
        }
        #endregion
    }

}