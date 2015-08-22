/**
Copyright (c) <2014>, <Devon Klompmaker>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/
using UnityEngine;
using System.Collections;
using Nodify.Runtime;
using Nodify.Editor;

namespace Nodify.Rendering
{
	public class NodeGroupRenderer : NodeRenderer<NodeGroup>
	{
		public override GUIStyle GetRenderStyle (NodeGroup node)
		{
			GUISkin editorSkin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");

            if (IsSelected(node))
            {
                return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Selected");
            }

            return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Group");
		}

        public override GUIContent GetContent(NodeGroup node)
        {
            return new GUIContent(node.gameObject.name, Resources.Load<Texture>("Icons/node_group_icon"));
        }

        public override void OnDrawBottomContextMenu(NodeGroup node, UnityEditor.GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Edit Group"), false, delegate
            {
                NodifyEditorUtilities.currentSelectedGroup = node.GetComponent<NodeGroup>();
            });

            menu.AddSeparator("");

            if (node.parent != null)
            {
                menu.AddItem(new GUIContent("Explode"), false, delegate
                {
                    foreach(Node child in node.childNodes)
                    {
                        child.transform.parent = node.parent.transform;

                        NodifyEditorWindow.AddToSelectedObjects(child.gameObject);
                    }

                    foreach(NodeGroup childGroup in node.childGroups)
                    {
                        childGroup.transform.parent = node.parent.transform;

                        NodifyEditorWindow.AddToSelectedObjects(childGroup.gameObject);
                    }

                    GameObject.DestroyImmediate(node.gameObject);

                    return;
                });
            }

            base.OnDrawBottomContextMenu(node, menu);
        }
	}
}
