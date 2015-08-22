using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Nodify.Editor
{
    public class NodifyGlobalVariableWizard : EditorWindow
    {
        private string menuPath = "Variables/GameObject";
        private string variableType = "GameObject";
        private bool openInEditor = false;

        private void OnGUI()
        {
            GUILayout.Label("Global Variable Wizard", EditorStyles.largeLabel);

            menuPath = EditorGUILayout.TextField("Context Menu Path", menuPath);
            variableType = EditorGUILayout.TextField("Variable Type", variableType);
            openInEditor = EditorGUILayout.Toggle("Open in Editor", openInEditor);

            if(GUILayout.Button("Create", EditorStyles.toolbarButton))
            {
                TextAsset template = Resources.Load<TextAsset>("Templates/NodifyNewVariableTemplate");

                if(template != null)
                {
                    string codeTemplate = template.text;

                    codeTemplate = codeTemplate.Replace("{menuPath}", menuPath);
                    codeTemplate = codeTemplate.Replace("{className}", menuPath.Replace("/", string.Empty));
                    codeTemplate = codeTemplate.Replace("{type}", variableType);

                    string path = NodifyEditorUtilities.GetCurrentAssetPath();

                    path += menuPath.Replace("/", string.Empty) + ".cs";

                    System.IO.File.WriteAllText(path, codeTemplate);

                    AssetDatabase.Refresh();

                    if (openInEditor)
                    {
                        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)));
                    }

                    this.Close();
                }
            }
        }
    }
}
