using UnityEngine;
using UnityEditor;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Editor
{
	[InitializeOnLoad]
    public static class NodifyHierarchyGUI
    {
		private static Texture m_iconNormal;
		private static Texture m_iconSelected;

        static NodifyHierarchyGUI()
        {
            UnityEditor.EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        public static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj != null)
            {
				NodeGroup nodeGroup = obj.GetComponent<NodeGroup>();
                if (nodeGroup != null)
                {
                	Rect iconRect = new Rect(selectionRect.xMax - 15, selectionRect.y + 4, 10, 10);

					ValidateIcons();
                    if (nodeGroup == NodifyEditorUtilities.currentSelectedGroup)
                    {
						if(m_iconSelected != null)
                        	GUI.DrawTexture(iconRect, m_iconSelected);
                    }
                    else
                    {
						if(m_iconNormal != null)
                        	GUI.DrawTexture(iconRect, m_iconNormal);
                    }
                }
            }
        }

		private static void ValidateIcons()
		{
			if(m_iconNormal == null)
			{
				m_iconNormal = Resources.Load<Texture>("Icons/node_group_icon_hierarchy");
			}
			if(m_iconSelected == null)
			{
				m_iconSelected = Resources.Load<Texture>("Icons/node_group_icon_hierarchy_selected");
			}
		}
    }
}