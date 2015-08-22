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
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using Nodify.Runtime;
using Nodify.Editor;

namespace Nodify.Rendering
{
    public class NodeRenderer<T> : NodeRendererBase where T : Node
    {
        /// <summary>
        /// Returns the GUIStyle that is used to render the 
        /// node on the graph.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
    	public virtual GUIStyle GetRenderStyle(T node)
    	{
    		GUISkin skin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");

            if(IsSelected(node))
            {
                return NodifyEditorUtilities.FindStyleByName(skin, "Node Selected");
            }

    		switch (node.editorExecutionState) 
    		{
				case ExecutionState.ERROR:
                    return NodifyEditorUtilities.FindStyleByName(skin, "Node Error");
				default:
                    return NodifyEditorUtilities.FindStyleByName(skin, "Node");
			}
    	}

        /// <summary>
        /// Determines if this node is currently selected.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool IsSelected(T node)
        {
            foreach(GameObject gameObject in Selection.gameObjects)
            {
                if(gameObject == node.gameObject)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the Rect of the node based on the render style
        /// and content.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
    	public virtual Rect GetContentRect(T node)
    	{
			GUIStyle renderingStyle = GetRenderStyle(node);

            Vector2 contentSize = renderingStyle.CalcSize(GetContent(node));
            contentSize.x = Mathf.Max(contentSize.x, 100);
            contentSize.y = Mathf.Max(contentSize.y, 50);

            Vector2 position = node.editorPosition + node.parent.editorWindowOffset;

            Rect contentRect = new Rect(position.x, position.y, contentSize.x, contentSize.y);

            return contentRect;
    	}

        /// <summary>
        /// Draws the GUI.Box node based on the GUIStyle and Content.
        /// </summary>
        /// <param name="node"></param>
        public virtual void OnRender(T node)
        {
            GUIStyle renderingStyle = GetRenderStyle(node);
            Rect contentRect = GetContentRect(node);

            GUI.Box(contentRect, GetContent(node), renderingStyle);
        }

        /// <summary>
        /// Returns the GUIContent for this node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual GUIContent GetContent(T node)
        {
            if(string.IsNullOrEmpty(node.editorResourceIcon))
            {
                return new GUIContent(node.gameObject.name);
            }
            else
            {
                return new GUIContent(node.gameObject.name, Resources.Load<Texture>(node.editorResourceIcon));
            }
        	
        }

        /// <summary>
        /// Entry point for rendering.
        /// </summary>
        /// <param name="node"></param>
        public void Render(T node)
        {
        	this.OnRender(node);
        	this.OnHandleEvents(node);

        	node.EditorUpdate();
        }

        /// <summary>
        /// Handles all mouse & keyboard events for the node.
        /// </summary>
        /// <param name="node"></param>
        public virtual void OnHandleEvents(T node)
        {
            node.editorPosition.x = Mathf.Max(node.editorPosition.x, 0f);
            node.editorPosition.y = Mathf.Max(node.editorPosition.y, 0f);
        	Rect contentRect = GetContentRect(node);

			EditorGUIUtility.AddCursorRect(contentRect, MouseCursor.MoveArrow);

        	if(contentRect.Contains(Event.current.mousePosition))
        	{
        		if(Event.current.type == EventType.ContextClick)
        		{
                    // Begin Expose default method
                    if (NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.auto_create_default_method_anchor_dor_node", 303)))
                    {
                        Expose defaultExposed = null;

                        foreach(Expose exposedElement in node.GetExposed())
                        {
                            if(exposedElement.isDefault)
                            {
                                defaultExposed = exposedElement;
                            }
                        }

                        if(defaultExposed == null)
                        {
                            defaultExposed = node.GetExposedByName("OnComplete");
                        }

                        if(defaultExposed != null)
                        {
                            GameObject exposed = new GameObject(defaultExposed.exposedName);
                            exposed.hideFlags = HideFlags.HideInHierarchy;

                            Anchor anchor = exposed.AddComponent<Anchor>();
                            anchor.displayName = defaultExposed.exposedName;

                            if(defaultExposed.exposedType == ExposedType.FIELD || defaultExposed.exposedType == ExposedType.PROPERTY)
                            {
                                anchor.type = AnchorType.VARIABLE;
                            }
                            else
                            {
                                anchor.type = AnchorType.METHOD;
                            }

                            anchor.editorPosition.y = -25;

                            exposed.transform.parent = node.transform;
                        }

                    }
                    else
                    {
                        if (NodifyEditorUtilities.currentConnectingAnchor == null)
                        {
                            GenericMenu menu = new GenericMenu();
                            this.OnDrawTopContextMenu(node, menu);
                            this.OnDrawBottomContextMenu(node, menu);
                            menu.ShowAsContext();
                        }
                        else
                        {
                            if (NodifyEditorUtilities.currentConnectingAnchor.type == AnchorType.METHOD)
                            {
                                NodifyEditorUtilities.currentConnectingAnchor.ConnectToNode(node);
                                NodifyEditorUtilities.currentConnectingAnchor = null;
                            }
                        }
                    }

        			Event.current.Use();
        		}

        		if(Event.current.type == EventType.MouseDrag && NodifyEditorUtilities.currentDraggingAnchor == null)
        		{
        			Event.current.Use();
        		}

        		if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
        		{
                    // Handle Hot Control for input manipulation
                    if(IsSelected(node))
                    {
                        if (NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.multiple_select_and_deselect", 303)))
                        {
                            NodifyEditorWindow.RemoveFromSelectedObjects(node.gameObject);
                            NodifyEditorWindow.ForceRepaint();
                        }
                        else
                        {
                            NodifyEditorUtilities.currentManipulatingNode = node;
                            NodifyEditorUtilities.currentManipulatingNodeOffset = node.editorPosition - Event.current.mousePosition;
                            
                        }
                        
                    }
                    else
                    {
                        NodifyEditorUtilities.currentManipulatingNode = node;
                        NodifyEditorUtilities.currentManipulatingNodeOffset = node.editorPosition - Event.current.mousePosition;

                        if (NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.multiple_select_and_deselect", 303)))
                        {
                            NodifyEditorWindow.AddToSelectedObjects(node.gameObject);
                            NodifyEditorWindow.ForceRepaint();
                        }
                        else
                        {
                            Selection.activeGameObject = node.gameObject;
                        }
                    }

                    Event.current.Use();
        		}
        	}

            if(NodifyEditorUtilities.currentManipulatingNode == node)
            {
                Vector2 lastPosition = node.editorPosition;
                
                node.editorPosition = Event.current.mousePosition + NodifyEditorUtilities.currentManipulatingNodeOffset;

                Vector2 moveDelta = node.editorPosition - lastPosition;

                NodifyEditorWindow.MovedNodeSelectionDelta(node, moveDelta);
            }

            if(Event.current.type == EventType.MouseUp)
            {
                if(NodifyEditorUtilities.currentManipulatingNode == node)
                {
                    NodifyEditorUtilities.currentManipulatingNode = null;
                }
            }
        }

        /// <summary>
        /// Draws the top portion of the context menu, usually reserved for expose
        /// methods.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="menu"></param>
        public virtual void OnDrawTopContextMenu(T node, GenericMenu menu)
        {
            foreach (Expose exposed in node.GetExposed())
            {
                if (exposed.exposedType != ExposedType.FIELD) { continue; }

                object[] argsArray = new object[] { exposed.exposedName, node.transform };

                menu.AddItem(new GUIContent("Expose/Field/" + exposed.exposedName + " [" + exposed.fieldInfo.Name + "]"), false, delegate(object args)
                {
                    object[] argsToArray = (object[])args;
                    GameObject exposedG = new GameObject((string)argsToArray[0]);
                    exposedG.hideFlags = HideFlags.HideInHierarchy;

                    Anchor anchor = exposedG.AddComponent<Anchor>();
                    anchor.displayName = (string)argsToArray[0];
                    anchor.editorPosition.y = -25;

                    exposedG.transform.parent = (Transform)argsToArray[1];
                }, argsArray);
            }

            foreach (Expose exposed in node.GetExposed())
            {
                if (exposed.exposedType != ExposedType.PROPERTY) { continue; }

                object[] argsArray = new object[] { exposed.exposedName, node.transform };

                menu.AddItem(new GUIContent("Expose/Property/" + exposed.exposedName + " [" + exposed.propertyInfo.Name + "]"), false, delegate(object args)
                {
                    object[] argsToArray = (object[])args;
                    GameObject exposedG = new GameObject((string)argsToArray[0]);
                    exposedG.hideFlags = HideFlags.HideInHierarchy;

                    Anchor anchor = exposedG.AddComponent<Anchor>();
                    anchor.displayName = (string)argsToArray[0];
                    anchor.editorPosition.y = -25;

                    exposedG.transform.parent = (Transform)argsToArray[1];
                }, argsArray);
            }

            foreach (Expose exposed in node.GetExposed())
            {
                if (exposed.exposedType != ExposedType.METHOD) { continue; }

                object[] argsArray = new object[] { exposed.exposedName, node.transform };

                menu.AddItem(new GUIContent("Expose/Method/" + exposed.exposedName), false, delegate(object args)
                {
                    object[] argsToArray = (object[])args;
                    GameObject exposedG = new GameObject((string)argsToArray[0]);
                    exposedG.hideFlags = HideFlags.HideInHierarchy;

                    Anchor anchor = exposedG.AddComponent<Anchor>();
                    anchor.displayName = (string)argsToArray[0];
                    anchor.type = AnchorType.METHOD;
                    anchor.editorPosition.y = -25;

                    exposedG.transform.parent = (Transform)argsToArray[1];
                }, argsArray);
            }

            menu.AddItem(new GUIContent("Add/New Comment"), false, delegate
            {
                GameObject comment = new GameObject("comment");
                comment.hideFlags = HideFlags.HideInHierarchy;

                Anchor anchor = comment.AddComponent<Anchor>();
                anchor.type = AnchorType.COMMENT;

                comment.transform.parent = node.transform;
            });

            menu.AddSeparator(string.Empty);
        }

        /// <summary>
        /// Draws the bottom portion of the context menu. Generally reserved for 
        /// deleting and other utility methods.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="menu"></param>
        public virtual void OnDrawBottomContextMenu(T node, GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Duplicate"), false, delegate
            {
                GameObject result = NodifyEditorUtilities.Duplicate(node.gameObject);

                result.GetComponent<Node>().editorPosition.y += 60;
            });

            menu.AddItem(new GUIContent("Delete"), false, delegate
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "Do you wish to delete the node: " + node.gameObject.name, "Delete", "Cancel"))
                {
                    NodifyEditorUtilities.SafeDestroy(node.gameObject);
                }
            });
        }
    }

	public class DefaultNodeRenderer : NodeRenderer<Node> { }

    public class NodeRendererBase { }

}
