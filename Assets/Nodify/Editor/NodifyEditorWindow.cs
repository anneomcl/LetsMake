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
using System.Collections.Generic;
using System.IO;
using Nodify.Runtime;
using System.Reflection;
using Nodify.Editor;

public class NodifyEditorWindow : SearchableEditorWindow
{   
	private const string WIKI_URL = "https://bitbucket.org/dklompmaker/nodify/wiki/Home";
	private const string ISSUES_URL = "https://bitbucket.org/dklompmaker/nodify/issues/new";

    #region GUIStyles
    private GUIStyle editorBreadcrumbLabel;
    private GUIStyle editorBreadcrumbLabelCurrent;
    private GUIStyle editorBackground;
    private GUIStyle editorSelectionRect;
    #endregion

    #region Private Fields
    private List<CreateMenu> createNodeMenuItems;
    private Vector2 breadcrumbsScrollOffset;
    private Vector2 lastMousePosition;
    /// <summary>
    /// Locked by default.
    /// </summary>
    private bool GroupLockButton = true;
    private Vector2 MouseSelectionStartPoint = new Vector2();
    private bool MouseSelection = false;
	private static float minimumZoomFactor = .5f;
	private static float maximumZoomFactor = 1;

	[SerializeField]
	private GUISkin guiSkin;
	[SerializeField]
	private Texture gridTexture;
	[SerializeField]
	private Texture logo;
    #endregion

    #region Static Helper Methods

    [MenuItem("Window/Nodify/Node Editor", false, 0)]
	public static NodifyEditorWindow Initialize()
	{
		return (NodifyEditorWindow)EditorWindow.GetWindow<NodifyEditorWindow>("Nodify");
	}

	public static void OpenInEditor(NodeGroup group)
	{
		Initialize();
        NodifyEditorUtilities.currentSelectedGroup = group;
	}

    [MenuItem("Assets/Create/Nodify/New Node Type", false, 8)]
    public static void CreateNewNode()
    {
        EditorWindow.GetWindow<NodifyNodeWizard>(true, "Create Type");
    }

    [MenuItem("Assets/Create/Nodify/New Global Variable Type", false, 8)]
    public static void CreateGlobalVariable()
    {
        EditorWindow.GetWindow<NodifyGlobalVariableWizard>(true, "Create Type");
    }

    public static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        string projectName = s[s.Length - 2];
        return projectName;
    }

    [MenuItem("Window/Nodify/Create Node Group", false, 1)]
    public static void CreateNodeGroup()
    {
        GameObject nodeGroup = new GameObject("NodeGroup");
        NodeGroup groupClass = nodeGroup.AddComponent<NodeGroup>();
        groupClass.editorPosition = new Vector2(20, 100);
        
        if(Selection.activeGameObject != null)
        {
            nodeGroup.transform.parent = Selection.activeTransform;
        }

        Selection.activeGameObject = nodeGroup;

        OpenInEditor(nodeGroup.GetComponent<NodeGroup>());
    }

	[MenuItem("Window/Nodify/Documentation", false, 100)]
	private static void OpenDocumentation()
	{
		Help.BrowseURL(WIKI_URL);
	}

	[MenuItem("Window/Nodify/Report a Bug...", false, 101)]
	private static void ReportBug()
	{
		Help.BrowseURL(ISSUES_URL);
	}

    #endregion

    public override void OnEnable()
    {
 	    base.OnEnable();

		if(guiSkin == null)
		{
			guiSkin = Resources.Load<GUISkin>("Styles/Nodify2EditorSkin");
		}
		if(gridTexture == null)
		{
			gridTexture = Resources.Load<Texture>("Images/editor_grid_background");
		}
		if(logo == null)
		{
			logo = Resources.Load<Texture>("Images/nodify_logo");
		}

		InitializeGUIStyles();
		InitializeCreateNodeMenu();
        EditorApplication.playmodeStateChanged += Repaint;
    }

    public override void OnDisable()
    {
        base.OnDisable();
		EditorApplication.playmodeStateChanged -= Repaint;
    }

	private void OnDestroy()
	{
		guiSkin = null;
		gridTexture = null;
		logo = null;
		Resources.UnloadUnusedAssets();
	}

    private void OnSelectionChange()
    {
        if (GroupLockButton == false || NodifyEditorUtilities.currentSelectedGroup == null)
        {
            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<NodeGroup>() != null)
            {
                if (NodifyEditorUtilities.currentManipulatingNode != null)
                {
                    if (NodifyEditorUtilities.currentManipulatingNode.gameObject.GetComponent<NodeGroup>() != Selection.activeGameObject.GetComponent<NodeGroup>())
                    {
                        OpenInEditor(Selection.activeGameObject.GetComponent<NodeGroup>());
                    }
                }
                else { OpenInEditor(Selection.activeGameObject.GetComponent<NodeGroup>()); }
            }
        }
        else if (GroupLockButton == true && NodifyEditorUtilities.currentSelectedGroup == null)
        {
            GroupLockButton = false;
            OnSelectionChange();
        }
        this.Repaint();
    }

    private void ShowButton(Rect rect)
    {
        if (GUI.Toggle(rect, GroupLockButton, GUIContent.none, "IN LockButton") != GroupLockButton)
        {
            GroupLockButton = !GroupLockButton;
            OnSelectionChange();
        }
    }

    private void InitializeCreateNodeMenu()
    {
        if(createNodeMenuItems == null)
			createNodeMenuItems = NodifyEditorUtilities.FindNodeTypes();
    }

	private void InitializeGUIStyles()
	{
		if(editorBreadcrumbLabel == null)
        	editorBreadcrumbLabel = NodifyEditorUtilities.FindStyleByName(guiSkin, "Editor Breadcrumb Label");
		if(editorBreadcrumbLabelCurrent == null)
        	editorBreadcrumbLabelCurrent = NodifyEditorUtilities.FindStyleByName(guiSkin, "Editor Breadcrumb Label Current");
		if(editorBackground == null)
        	editorBackground = NodifyEditorUtilities.FindStyleByName(guiSkin, "Editor Background");
		if(editorSelectionRect == null)
        	editorSelectionRect = NodifyEditorUtilities.FindStyleByName(guiSkin, "Editor Selection Rect");
	}

	private void AssignSelectedGroup()
	{
		if(NodifyEditorUtilities.currentSelectedGroup != null)
			return;

		if(Selection.activeGameObject != null)
		{
			NodeGroup group = Selection.activeGameObject.GetComponent<NodeGroup>();
			if(group == null)
			{
				if(Selection.activeGameObject.GetComponent<Anchor>())
				{
					Selection.activeTransform = Selection.activeTransform.parent;
				}

				if(Selection.activeGameObject.GetComponent<Node>())
				{
					group = Selection.activeGameObject.GetComponent<Node>().parent;
				}
				
			}

            NodifyEditorUtilities.currentSelectedGroup = group;
		}
	}

    private void OnDrawEditorGrid()
    {
		float percentageOfSize = (1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount);

        float sWidth = this.position.width + (this.position.width * percentageOfSize);
        float sHeight = this.position.height + (this.position.height * percentageOfSize);

        float gWidth = gridTexture.width;
        float gHeight = gridTexture.height;

        for (float x = NodifyEditorUtilities.currentSelectedGroup.editorWindowOffset.x; x < sWidth; x += gWidth)
        {
            for (float y = NodifyEditorUtilities.currentSelectedGroup.editorWindowOffset.y; y < sHeight; y += gHeight)
            {
                GUI.DrawTexture(new Rect(x, y, gWidth, gHeight), gridTexture);
            }
        }
    }
	
    private void OnDrawEditorBackground()
    {
        GUI.Box(new Rect(0, 0, this.position.width, this.position.height), string.Empty, this.editorBackground);
    }

	public void OnGUI()
	{
		AssignSelectedGroup();
        
        NodeGroup selectedGroup = NodifyEditorUtilities.currentSelectedGroup;
        if (selectedGroup != null)
        {
            selectedGroup.SetHideStateChildrenNonNodes(HideFlags.None);

            OnDrawEditorBackground();

            NodifyEditorUtilities.BeginZoomArea(selectedGroup.editorZoomAmount, new Rect(0, 0, Screen.width, Screen.height));

            this.RemoveNotification();

            selectedGroup.editorWindowOffset.x = Mathf.Min(selectedGroup.editorWindowOffset.x, 0);
            selectedGroup.editorWindowOffset.y = Mathf.Min(selectedGroup.editorWindowOffset.y, 0);

            this.OnDrawEditorGrid();

            if(selectedGroup != null)
            {
                if (NodifyEditorUtilities.currentConnectingAnchor != null)
                {
                    DrawCurrentConnectingAnchor(NodifyEditorUtilities.currentConnectingAnchor);
                }

                /// Begin Node Anchor Rendering (Anchors rendered behind everything)
    			foreach(Node node in selectedGroup.childNodes)
            	{
    				foreach(Anchor anchor in node.anchors)
            		{
            			for(int i = 0; i < anchor.anchorConnections.Count; i++)
            			{
                            if (anchor.anchorConnections[i] == null) { continue; }

							DrawAnchorConnection(anchor.anchorConnections[i]);
            			}

						for(int i = 0; i < anchor.nodeConnections.Count; i++)
            			{
							if (anchor.nodeConnections[i] == null) { continue; }

							DrawNodeConnection(anchor.nodeConnections[i]);
                        }
            		}
            	}
                // End Node Anchor Rendering

                //Begin Node Group Anchor Rendering
                foreach (NodeGroup nodeGroup in selectedGroup.childGroups)
                {
                    foreach (Anchor anchor in nodeGroup.anchors)
                    {
                        foreach (AnchorConnection connection in anchor.anchorConnections)
                        {
                            DrawAnchorConnection(connection);
                        }

                        foreach (NodeConnection connection in anchor.nodeConnections)
                        {
                            DrawNodeConnection(connection);
                        }
                    }
                }
                // End Node Group Anchor Rendering

                // Node Rendering
    			foreach(Node node in selectedGroup.childNodes)
            	{
    				if(node.GetComponent<NodeGroup>()) { continue; }

    				foreach(Anchor anchor in node.anchors)
            		{
            			DrawAnchor(anchor);
            		}

            		DrawNode(node);
            	}
                // End Node Rendering

                // Node Group Rendering
            	foreach(NodeGroup nodeGroup in selectedGroup.childGroups)
            	{
            		if(nodeGroup == selectedGroup) { continue; }

                    foreach (Anchor anchor in nodeGroup.anchors)
                    {
                        DrawAnchor(anchor);
                    }

            		DrawNodeGroup(nodeGroup);
            	}
                // End Node Group Rendering
            }

            NodifyEditorUtilities.EndZoomArea();

            HandleEditorEvents(selectedGroup);

            if (MouseSelection == true && Event.current.type == EventType.Repaint)
            {
                editorSelectionRect.Draw(RectExtensions.GetRectFromPoints(MouseSelectionStartPoint, Event.current.mousePosition), false, false, false, false);
            }
        }
        else
        {
            ShowNotification(new GUIContent("Create or Select a Node Group"));
        }

        DrawToolbar();
        DrawBreadcrumbs();
        DrawLogo();

        if (ShouldRepaint())
        {
            this.Repaint();
        }
	}

    public static void ForceRepaint()
    {
        GetWindow<NodifyEditorWindow>().Repaint();
        EditorApplication.RepaintHierarchyWindow();
    }

    public bool ShouldRepaint()
    {
        if (Event.current.type == EventType.used || Event.GetEventCount() > 0 || EditorPrefs.GetBool("nodify.automatic_window_repaint", false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Called when a node is moved on the editor graph.
    /// </summary>
    /// <param name="delta"></param>
    public static void MovedNodeSelectionDelta(Node node, Vector2 delta)
    {
        foreach(GameObject gameObject in Selection.gameObjects)
        {
            if(gameObject.GetComponent<Node>() && gameObject.GetComponent<Node>() != node)
            {
                gameObject.GetComponent<Node>().editorPosition += delta;
            }
        }
    }

    public static void AddToSelectedObjects(GameObject obj)
    {
        if (!Selection.Contains(obj))
        {
            List<GameObject> gObj = new List<GameObject>();
            gObj.AddRange(Selection.gameObjects);
            gObj.Add(obj);

            Selection.objects = gObj.ToArray();
            
        }
    }

    public static void RemoveFromSelectedObjects(GameObject obj)
    {
        if(Selection.Contains(obj))
        {
            List<GameObject> gObj = new List<GameObject>();
            gObj.AddRange(Selection.gameObjects);
            gObj.Remove(obj);

            Selection.objects = gObj.ToArray();
        }
    }

    private void SelectNodesInRect(Rect rect)
    {
        rect.x *= 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
        rect.y *= 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
        rect.width *= 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;
        rect.height *= 1f / NodifyEditorUtilities.currentSelectedGroup.editorZoomAmount;

        if (!Event.current.shift && EditorPrefs.GetInt("nodify.hotkeys.multi_select_nodes", 303) == 0)
        {
            Selection.activeGameObject = null;
        }
        
        foreach (Node childNodes in NodifyEditorUtilities.currentSelectedGroup.childNodes)
        {
            object renderer = Nodify.Editor.NodifyEditorUtilities.FindNodeRenderer(childNodes.GetType());
            Rect NodesPosition = (Rect)renderer.GetType().GetMethod("GetContentRect").Invoke(renderer, new object[] { childNodes });

            Rect position = NodesPosition;
            if (position.xMax >= rect.x && position.x <= rect.xMax && position.yMax >= rect.y && position.y <= rect.yMax) {
                NodifyEditorWindow.AddToSelectedObjects(childNodes.gameObject);
            }
        }
        NodifyEditorWindow.ForceRepaint();
    }


    private void HandleEditorEvents(NodeGroup group)
    {
        int multi_select_nodes = EditorPrefs.GetInt("nodify.hotkeys.multi_select_nodes", 303);
        // Begin Context Menu Controls
        if (Event.current.type == EventType.ContextClick)
        {
            if (NodifyEditorUtilities.currentConnectingAnchor != null)
            {
                NodifyEditorUtilities.currentConnectingAnchor = null;

                Event.current.Use();
            }
            else
            {
                ShowNodeCreationMenu();

                Event.current.Use();
            }
        }

        
        if(Event.current.type == EventType.KeyDown)
        {
            // Begin Focus Element Control
            if (Event.current.keyCode == (KeyCode)EditorPrefs.GetInt("nodify.hotkeys.focus_on_selected_node2", 102) && NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.focus_on_selected_node", 0)))
            {
                if (Selection.activeGameObject != null)
                {
                    if (Selection.activeGameObject.GetComponent<Node>())
                    {
                        FocusNode(group, Selection.activeGameObject.GetComponent<Node>());
                    }

                    if (Selection.activeGameObject.GetComponent<Anchor>())
                    {
                        FocusNode(group, Selection.activeGameObject.GetComponent<Anchor>().parent);
                    }
                }
                ForceRepaint();
            }

            if (Event.current.keyCode == KeyCode.Delete)
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (obj != NodifyEditorUtilities.currentSelectedGroup.gameObject && (obj.GetComponent<Node>() || obj.GetComponent<Anchor>()))
                    {
                        if (EditorUtility.DisplayDialog("Are you sure?", "Do you wish to delete the node: " + obj.name, "Delete", "Cancel"))
                        {
                            NodifyEditorUtilities.SafeDestroy(obj);
                        }
                    }
                }
            }
        }

        // Begin Mouse Selection Events
        if (NodifyEditorUtilities.currentManipulatingNode == null && NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.multi_select_nodes", 303)))
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    if (Event.current.button == 0)
                    {
                        MouseSelectionStartPoint = Event.current.mousePosition;
                        MouseSelection = true;
                        ForceRepaint();
                    }
                    break;
                case EventType.MouseUp:
                    if (MouseSelection == true)
                    {
                        SelectNodesInRect(RectExtensions.GetRectFromPoints(MouseSelectionStartPoint, Event.current.mousePosition));
                        MouseSelection = false;
                    }
                    ForceRepaint();
                    break;
                case EventType.MouseDrag:
                    if (Event.current.button == 0)
                    {
                        ForceRepaint();
                    }
                    else if (Event.current.button == 2) // Begin Mouse Drag Controls
                    {
                        Vector2 mouseDelta = Event.current.delta;
                        Vector2 mouseZoomDelta = (1f / group.editorZoomAmount) * mouseDelta;

                        group.editorWindowOffset += mouseZoomDelta;

                        Event.current.Use();
                    }
                    break;
            }
        }

        if (Event.current.type == EventType.MouseDrag && multi_select_nodes != 0 && !NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.multi_select_nodes", 303)))
        {
            Vector2 mouseDelta = Event.current.delta;
            Vector2 mouseZoomDelta = (1f / group.editorZoomAmount) * mouseDelta;

            group.editorWindowOffset += mouseZoomDelta;

            ForceRepaint();
        }

        // Begin Zoom Controls
        if (Event.current.type == EventType.ScrollWheel)
        {
            group.editorZoomAmount -= Event.current.delta.y / 80;
            group.editorZoomAmount = (float)System.Math.Round((double)Mathf.Clamp(group.editorZoomAmount, minimumZoomFactor, maximumZoomFactor), 2);

            Event.current.Use();
        }

        if (Event.current.type == EventType.MouseDown && multi_select_nodes != 0 && NodifyPreferencesGUI.GetAdditionalButtons(EditorPrefs.GetInt("nodify.hotkeys.deselect_all", 303)))
        {
            Selection.objects = new UnityEngine.Object[0];

            Event.current.Use();
        }

        // Begin Drag & Drop Components into Variables
        if(Event.current.type == EventType.DragPerform || Event.current.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Link;

            if(Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                foreach(UnityEngine.Object obj in DragAndDrop.objectReferences)
                {
                    foreach(CreateMenu nodeMenu in NodifyEditorUtilities.FindNodeTypes())
                    {
                        if(nodeMenu.type.BaseType.IsGenericType)
                        {
                            if(nodeMenu.type.BaseType.GetGenericArguments()[0] == obj.GetType())
                            {
                                GameObject nodeObj = new GameObject(obj.GetType().Name);

								#if UNITY_5
								Node nodeClass = (Node)nodeObj.AddComponent(nodeMenu.type);
								#else
								Node nodeClass = (Node)nodeObj.AddComponent(nodeMenu.type.Name);
								#endif

                                nodeClass.editorPosition = Event.current.mousePosition - group.editorWindowOffset;
                                nodeClass.editorResourceIcon = nodeMenu.iconResourcePath;
                                nodeClass.OnEditorNodeCreated();
                                nodeObj.transform.parent = group.transform;

                                nodeClass.GetType().GetField("value").SetValue(nodeClass, obj);
                            }
                        }
                    }
                }
            }
        }
    }

    public void FocusNode(NodeGroup group, Node node)
    {
        group.editorZoomAmount = maximumZoomFactor;
        group.editorWindowOffset = -node.editorPosition + new Vector2(this.position.width / 2, this.position.height / 2);
    }

    public void DrawLogo()
    {
		GUI.DrawTexture(new Rect(position.width - logo.width - 20, position.height - logo.height - 20, logo.width, logo.height), logo);
    }

	public void DrawToolbar()
	{
		GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));

		GUILayout.FlexibleSpace();

		if (NodifyEditorUtilities.currentSelectedGroup != null)
		{
			NodifyEditorUtilities.currentSelectedGroup.editorShowComments = GUILayout.Toggle(NodifyEditorUtilities.currentSelectedGroup.editorShowComments, "Comments", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false));
		}

        if (GUILayout.Button(new GUIContent("+ Create Node Group"), EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
        {
            CreateNodeGroup();
        }
        
        if (GUILayout.Button(new GUIContent(EditorGUIUtility.FindTexture("_Help"), "Open Wiki"), EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
        {
            Help.BrowseURL(WIKI_URL);
        }

		GUILayout.EndHorizontal();
	}

    public void DrawBreadcrumbs()
    {
        List<NodeGroup> trail = new List<NodeGroup>();

        if (NodifyEditorUtilities.currentSelectedGroup != null)
        {
            trail.Add(NodifyEditorUtilities.currentSelectedGroup);

            NodeGroup parent = NodifyEditorUtilities.currentSelectedGroup.parent;

            while(parent != null)
            {
                trail.Add(parent);

                parent = parent.parent;
            }
        }

        trail.Reverse();

        breadcrumbsScrollOffset = EditorGUILayout.BeginScrollView(breadcrumbsScrollOffset, false, false);

        GUILayout.BeginHorizontal( GUILayout.ExpandHeight(false));

        for (int i = 0; i < trail.Count; i++)
        {
            if (i < trail.Count - 1)
            {
                if (GUILayout.Button(trail[i].gameObject.name, editorBreadcrumbLabel))
                {
                    NodifyEditorUtilities.currentSelectedGroup = trail[i];

                    break;
                }
            }
            
            if(i == trail.Count - 1)
            {
                if (GUILayout.Button(trail[i].gameObject.name, editorBreadcrumbLabelCurrent))
                {
                    Selection.activeGameObject = trail[i].gameObject;
                }
            }
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }

	public void DrawNode(Node node)
	{
		object renderer = Nodify.Editor.NodifyEditorUtilities.FindNodeRenderer(node.GetType());
		renderer.GetType().GetMethod("Render").Invoke(renderer, new object[] { node });
	}


	public void DrawAnchor(Anchor anchor)
	{
		object renderer = Nodify.Editor.NodifyEditorUtilities.FindAnchorRenderer(anchor.GetType());
		renderer.GetType().GetMethod("Render").Invoke(renderer, new object[] { anchor });			
	}

	public void DrawNodeGroup(NodeGroup node)
	{
        object renderer = Nodify.Editor.NodifyEditorUtilities.FindNodeRenderer(node.GetType());
        renderer.GetType().GetMethod("Render").Invoke(renderer, new object[] { node });
	}

    public void DrawCurrentConnectingAnchor(Anchor anchor)
    {
        object renderer = Nodify.Editor.NodifyEditorUtilities.FindAnchorRenderer(anchor.GetType());
        Rect contentRect = (Rect)renderer.GetType().GetMethod("GetContentRect").Invoke(renderer, new object[] { anchor });		
        Rect targetPosition = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 2, 2);

        NodifyEditorUtilities.DrawBezier(contentRect.center, targetPosition.center, Color.black, 2, 75);

        this.Repaint();
    }

	public void DrawNodeConnection(NodeConnection connection)
	{
        object renderer = Nodify.Editor.NodifyEditorUtilities.FindAnchorRenderer(connection.owner.GetType());
        renderer.GetType().GetMethod("OnRenderNodeConnection").Invoke(renderer, new object[] { connection.owner, connection });
	}

	public void DrawAnchorConnection(AnchorConnection connection)
	{
        object renderer = Nodify.Editor.NodifyEditorUtilities.FindAnchorRenderer(connection.owner.GetType());
        renderer.GetType().GetMethod("OnRenderAnchorConnection").Invoke(renderer, new object[] { connection.owner, connection });
	}

    public void ShowNodeCreationMenu()
    {
        if (NodifyEditorUtilities.currentSelectedGroup == null) { return; }

        GenericMenu menu = new GenericMenu();

		InitializeCreateNodeMenu();
		foreach(CreateMenu menuItem in createNodeMenuItems)
        {
            object[] argArray = new object[] { menuItem.type, menuItem.name, Event.current.mousePosition - NodifyEditorUtilities.currentSelectedGroup.editorWindowOffset, menuItem.iconResourcePath };

            menu.AddItem(new GUIContent(menuItem.path), false, delegate(object args)
            {
                object[] argToArray = (object[])args;

                System.Type type = (System.Type)argToArray[0];
                GameObject nodeObj = new GameObject((string)argToArray[1]);

				#if UNITY_5
					Node nodeClass = (Node)nodeObj.AddComponent(type);
				#else
					Node nodeClass = (Node)nodeObj.AddComponent(type.Name);
				#endif
                nodeClass.editorPosition = (Vector2)argToArray[2];
                nodeClass.editorResourceIcon = (string)argToArray[3];
                nodeClass.OnEditorNodeCreated();
                nodeObj.transform.parent = NodifyEditorUtilities.currentSelectedGroup.transform;
                
               

            }, argArray);
        }

        menu.AddSeparator("");

        menu.AddItem(new GUIContent("New Group"), false, delegate(object args)
        {
            GameObject nGroup = new GameObject("NewGroup");
            NodeGroup nGroupClass = nGroup.AddComponent<NodeGroup>();
            nGroupClass.transform.parent = NodifyEditorUtilities.currentSelectedGroup.transform;
            nGroupClass.editorPosition = (Vector2)args;
            nGroupClass.OnEditorNodeCreated();
        }, Event.current.mousePosition - NodifyEditorUtilities.currentSelectedGroup.editorWindowOffset);
        
        if(Selection.objects.Length > 0)
        {
            if(Selection.objects.Length > 1 || (Selection.objects.Length == 1 && !Selection.Contains(NodifyEditorUtilities.currentSelectedGroup.gameObject)))
            {
                menu.AddItem(new GUIContent("New Group [with selected]"), false, delegate(object args)
                {
                    GameObject nGroup = new GameObject("NewGroup");
                    NodeGroup nGroupClass = nGroup.AddComponent<NodeGroup>();
                    nGroupClass.transform.parent = NodifyEditorUtilities.currentSelectedGroup.transform;
                    nGroupClass.editorPosition = (Vector2)args;
                    nGroupClass.OnEditorNodeCreated();

                    foreach(GameObject obj in Selection.gameObjects)
                    {
                        if(obj != NodifyEditorUtilities.currentSelectedGroup.gameObject && obj.GetComponent<Node>())
                        {
                            obj.transform.parent = nGroupClass.transform;
                        }
                    }

                }, Event.current.mousePosition - NodifyEditorUtilities.currentSelectedGroup.editorWindowOffset);
            }
        }

        menu.ShowAsContext();
    }

    #region Preference Nodify


    #endregion
}
