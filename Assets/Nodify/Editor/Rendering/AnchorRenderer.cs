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
    public class AnchorRenderer<T> : AnchorRendererBase where T : Anchor
    {
        public virtual GUIStyle GetRenderStyle(T anchor)
        {
            GUISkin editorSkin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");

            switch (anchor.type)
            {
                case AnchorType.VARIABLE:
                    if(anchor.exposedReference != null)
                    {
                        if(anchor.exposedReference.exposedType == ExposedType.FIELD)
                        {
                            return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Anchor Field");
                        }

                        if(anchor.exposedReference.exposedType == ExposedType.PROPERTY)
                        {
                            return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Anchor Property");
                        }
                    }
                break;
                case AnchorType.COMMENT:
                    return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Anchor Comment");
                case AnchorType.METHOD:
                    return NodifyEditorUtilities.FindStyleByName(editorSkin, "Node Anchor Method");
            }

            return null;
        }

        public virtual GUIContent GetContent(T anchor)
        {
            string contentString = anchor.displayName;

            if (anchor.type == AnchorType.VARIABLE)
            {
                FieldInfo parentField = anchor.parent.GetType().GetField(anchor.displayName);

                if (anchor.showValueInEditor)
                {
                    if (parentField != null)
                    {
                        object parentValue = parentField.GetValue(anchor.parent);

                        if (parentValue != null)
                        {
                            contentString += " [" + parentValue.ToString() + "]";
                        }
                    }
                }
            }

            if (anchor.type != AnchorType.COMMENT)
            {
                contentString = NodifyEditorUtilities.Truncate(contentString, 25);
            }

            if(anchor.exposedReference != null && !string.IsNullOrEmpty(anchor.exposedReference.iconPath))
            {
                return new GUIContent(Resources.Load<Texture>(anchor.exposedReference.iconPath));
            }
            else
            {
                return new GUIContent(contentString);
            }

            
        }

        public virtual Rect GetContentRect(T anchor)
        {
            GUIStyle contentStyle = GetRenderStyle(anchor);

            if (contentStyle != null)
            {
                Vector2 contentSize = contentStyle.CalcSize(new GUIContent(GetContent(anchor)));
                Vector2 position = anchor.editorPosition + anchor.parent.editorPosition + anchor.parent.parent.editorWindowOffset;
                Rect contentRect = new Rect(position.x, position.y, contentSize.x, contentSize.y);

                return contentRect;
            }

            return default(Rect);
        }

        public virtual void OnRender(T anchor)
        {
            object parentRenderer = NodifyEditorUtilities.FindNodeRenderer(anchor.parent.GetType());

            if (parentRenderer != null)
            {
                Rect parentRect = (Rect)parentRenderer.GetType().GetMethod("GetContentRect").Invoke(parentRenderer, new object[] { anchor.parent });
                Rect contentRect = GetContentRect(anchor);

                if (contentRect.size != Vector2.zero)
                {
                    float sizePercentage = 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
                    float lineSize = 2 * sizePercentage;

                    NodifyEditorUtilities.DrawBezier(parentRect.center, contentRect.center, Color.black, lineSize, 15);

                    GUIContent anchorContent = GetContent(anchor);
                    GUIStyle anchorStyle = GetRenderStyle(anchor);

                    if (anchorContent != null && anchorStyle != null)
                    {
                        GUI.Box(contentRect, GetContent(anchor), GetRenderStyle(anchor));
                    }
                }
            }
        }

        public void Render(T anchor)
        {
            if (anchor.type == AnchorType.COMMENT && !NodifyEditorUtilities.currentSelectedGroup.editorShowComments) { return; }

            this.OnRender(anchor);
            this.OnHandleEvents(anchor);
        }

        public virtual void OnHandleEvents(T anchor)
        {
            Rect contentRect = GetContentRect(anchor);

            EditorGUIUtility.AddCursorRect(contentRect, MouseCursor.MoveArrow);

            if (contentRect.Contains(Event.current.mousePosition) && NodifyEditorUtilities.currentManipulatingNode == null)
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    Selection.activeGameObject = anchor.gameObject;
                    NodifyEditorUtilities.currentDraggingAnchor = anchor;

                    Event.current.Use();
                }

                if (Event.current.type == EventType.ContextClick)
                {
                    if (NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.bring_up_anchors_menu", 303)))
                    {
                        GenericMenu menu = new GenericMenu();
                        this.OnDrawTopContextMenu(anchor, menu);
                        this.OnDrawBottomContextMenu(anchor, menu);
                        menu.ShowAsContext();
                    }
                    else
                    {
                        if (NodifyEditorUtilities.currentConnectingAnchor == null)
                        {
                            NodifyEditorUtilities.currentConnectingAnchor = anchor;
                        }
                        else
                        {
                            if (NodifyEditorUtilities.currentConnectingAnchor.parent != anchor.parent)
                            {
                                if (NodifyEditorUtilities.currentConnectingAnchor.type == AnchorType.VARIABLE && anchor.type == AnchorType.VARIABLE)
                                {
                                    NodifyEditorUtilities.currentConnectingAnchor.ConnectToAnchor(anchor);
                                    NodifyEditorUtilities.currentConnectingAnchor = null;
                                }
                            }
                        }
                    }

                    Event.current.Use();
                }
            }

            if (Event.current.type == EventType.MouseDrag && NodifyEditorUtilities.currentDraggingAnchor == anchor)
            {
                anchor.editorPosition += Event.current.delta;

                Event.current.Use();
            }

            if(Event.current.type == EventType.MouseUp && NodifyEditorUtilities.currentDraggingAnchor == anchor)
            {
                NodifyEditorUtilities.currentDraggingAnchor = null;

                Event.current.Use();
            }
        }

        public virtual void OnRenderAnchorConnection(T anchor, AnchorConnection connection)
        {
            if (connection.target == null || anchor.parent.parent != connection.target.parent.parent)
            {
                NodifyEditorUtilities.SafeDestroy(connection.gameObject);

                return;
            }

            Rect contentRect = GetContentRect(anchor);

            object targetRenderer = NodifyEditorUtilities.FindAnchorRenderer(connection.target.GetType());

            if (targetRenderer != null)
            {
                Rect targetRect = (Rect)targetRenderer.GetType().GetMethod("GetContentRect").Invoke(targetRenderer, new object[] { connection.target });

                Vector2 bezierStartPoint = contentRect.center;
                Vector2 bezierEndPoint = targetRect.center;

                float sizePercentage = 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
                float lineSize = 2 * sizePercentage;

                NodifyEditorUtilities.DrawBezier(bezierStartPoint, bezierEndPoint, Color.black, lineSize, 25);

                GUISkin skin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");
                GUIStyle minusButtonStyle = NodifyEditorUtilities.FindStyleByName(skin, "Node Anchor Remove");
                GUIStyle connectionArrowStyle = NodifyEditorUtilities.FindStyleByName(skin, "Node Anchor Field Connection Arrow");

                Vector2 centerPoint = NodifyEditorUtilities.PointOnBezier(bezierStartPoint, bezierEndPoint, .5f, 25);

                this.OnRenderAnchorConnectionMinusButton(anchor, connection, minusButtonStyle, connectionArrowStyle, centerPoint, bezierStartPoint, bezierEndPoint);
            }
        }

        public virtual void OnRenderNodeConnection(T anchor, NodeConnection connection)
        {
            if (connection.target == null || anchor.parent.parent != connection.target.parent)
            {
                NodifyEditorUtilities.SafeDestroy(connection.gameObject);

                return;
            }

            Rect contentRect = GetContentRect(anchor);

            object targetRenderer = NodifyEditorUtilities.FindNodeRenderer(connection.target.GetType());

            if (targetRenderer != null)
            {
                Rect targetRect = (Rect)targetRenderer.GetType().GetMethod("GetContentRect").Invoke(targetRenderer, new object[] { connection.target });

                Vector2 bezierStartPoint = NodifyEditorUtilities.ClosestPointOnRectangle(targetRect.center, contentRect);
                Vector2 bezierEndPoint = NodifyEditorUtilities.ClosestPointOnRectangle(bezierStartPoint, targetRect);

                if (anchor.type == AnchorType.METHOD)
                {
                    Vector2 towardsStartCenter = (contentRect.center - bezierStartPoint).normalized;
                    Vector2 towardsTargetCenter = (targetRect.center - bezierEndPoint).normalized;

                    bezierStartPoint = new Vector2(bezierStartPoint.x + (towardsStartCenter.x * 10), bezierStartPoint.y + (towardsStartCenter.y * 10));
                    bezierEndPoint = new Vector2(bezierEndPoint.x + (towardsTargetCenter.x * 10), bezierEndPoint.y + (towardsTargetCenter.y * 10));
                }

                Color bezierColor = Color.black;

                if (connection.state == ConnectionState.RUN)
                {
                    bezierColor = Color.green;
                }

                if (connection.state == ConnectionState.ERROR)
                {
                    bezierColor = Color.red;
                }

                float sizePercentage = 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
                float lineSize = 2 * sizePercentage;


                NodifyEditorUtilities.DrawBezier(bezierStartPoint, bezierEndPoint, bezierColor, lineSize, Mathf.Abs(bezierEndPoint.y - bezierStartPoint.y) / 2);

                GUISkin skin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");
                GUIStyle minusButtonStyle = NodifyEditorUtilities.FindStyleByName(skin, "Node Anchor Remove");
                GUIStyle connectionArrowStyle = NodifyEditorUtilities.FindStyleByName(skin, "Node Anchor Method Connection Arrow");

                Vector2 centerPoint = NodifyEditorUtilities.PointOnBezier(bezierStartPoint, bezierEndPoint, .5f, Mathf.Abs(bezierEndPoint.y - bezierStartPoint.y) / 2);

                this.OnRenderNodeConnectionMinusButton(anchor, connection, minusButtonStyle, connectionArrowStyle, centerPoint, bezierStartPoint, bezierEndPoint);
            }
        }

        public virtual void OnRenderNodeConnectionMinusButton(Anchor anchor, NodeConnection connection, GUIStyle minusStyle, GUIStyle arrowStyle, Vector2 centerPoint, Vector2 start, Vector2 end)
        {
            Rect minusRect = new Rect(centerPoint.x - 8, centerPoint.y - 8, 16, 16);

            GUI.Box(minusRect, string.Empty, minusStyle);


            EditorGUIUtility.AddCursorRect(minusRect, MouseCursor.Link);

            if (minusRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button != 2)
                {
                    anchor.RemoveConnection(connection);
                    NodifyEditorUtilities.SafeDestroy(connection.gameObject);
                    NodifyEditorWindow.ForceRepaint();
                }
            }
        }

        public virtual void OnRenderAnchorConnectionMinusButton(Anchor anchor, AnchorConnection connection, GUIStyle minusStyle, GUIStyle arrowStyle, Vector2 centerPoint, Vector2 start, Vector2 end)
        {
            Rect minusRect = new Rect(centerPoint.x - 8, centerPoint.y - 8, 16, 16);

            GUI.Box(minusRect, string.Empty, minusStyle);

            EditorGUIUtility.AddCursorRect(minusRect, MouseCursor.Link);

            if (minusRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button != 2)
                {
                    anchor.RemoveConnection(connection);
                    NodifyEditorUtilities.SafeDestroy(connection.gameObject);
                    NodifyEditorWindow.ForceRepaint();
                }
            }
        }

        public virtual void OnDrawTopContextMenu(T anchor, GenericMenu menu)
        {

        }

        public virtual void OnDrawBottomContextMenu(T anchor, GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Delete"), false, delegate
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "Do you wish to delete the anchor: " + anchor.displayName, "Delete", "Cancel"))
                {
                    NodifyEditorUtilities.SafeDestroy(anchor.gameObject);
                }
            });
        }
    }

    public class DefaultAnchorRenderer : AnchorRenderer<Anchor> { }

    public class AnchorRendererBase { }
}
