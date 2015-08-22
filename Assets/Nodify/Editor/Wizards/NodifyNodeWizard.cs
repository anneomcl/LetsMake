using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Nodify.Editor
{
    public class NodifyNodeWizard : EditorWindow
    {
        private string menuPath = "Unity/GameObject/Find";
        private string displayName = "GameObject.Find";
        private string iconResourcePath = string.Empty;
        private bool openInEditor = false;

        private void OnGUI()
        {
            GUILayout.Label("Node Wizard", EditorStyles.largeLabel);

            menuPath = EditorGUILayout.TextField(new GUIContent("Context Menu Path", "The full path for instance: Unity/GameObject/Find"), menuPath);
            displayName = EditorGUILayout.TextField(new GUIContent("Display Name", "The name seen on the node graph. For instance: GameObject.Find"), displayName);
            iconResourcePath = EditorGUILayout.TextField("Icon Resource Path [optional]", iconResourcePath);
            openInEditor = EditorGUILayout.Toggle(new GUIContent("Open in Editor", "Do you want to open this script in your editor?"), openInEditor);

            string className = menuPath.Replace("/", string.Empty).Replace(" ", string.Empty).Replace(".", string.Empty);

            if(GUILayout.Button("Create", GUILayout.Height(22.0f)))
            {
                TextAsset template = Resources.Load<TextAsset>("Templates/NodifyNewNodeTemplate");

                if(template != null)
                {
                    string codeTemplate = template.text;

                    codeTemplate = codeTemplate.Replace("{menuPath}", menuPath);
                    codeTemplate = codeTemplate.Replace("{className}", className);
                    codeTemplate = codeTemplate.Replace("{displayName}", displayName);

                    if(!string.IsNullOrEmpty(iconResourcePath))
                    {
                        char s = '"';
                        codeTemplate = codeTemplate.Replace("{icon}", ", " + s + iconResourcePath + s);
                    }
                    else
                    {
                        codeTemplate = codeTemplate.Replace("{icon}", string.Empty);
                    }

                    string path = NodifyEditorUtilities.GetCurrentAssetPath();

                    path += className + ".cs";

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
