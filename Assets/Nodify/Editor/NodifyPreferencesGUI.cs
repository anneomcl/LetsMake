using UnityEngine;
using UnityEditor;
using System.Collections;


public class NodifyPreferencesGUI
{
    #region Private Fields
    private static bool ShowHotKeysSettings = false;
    private static Vector2 PreferencesScrollView = new Vector2();
    #endregion

    #region AdditionalButtons
    public enum AdditionalButtons { None = 0, Shift = 303, Alt = 307, Control = 305 }

    public enum AdditionalButtonsNotNone { Shift = 303, Alt = 307, Control = 305 }

    public static bool GetAdditionalButtons(int ButtonsKeyCode)
    {
        return GetAdditionalButtons((AdditionalButtons)ButtonsKeyCode);
    }

    public static bool GetAdditionalButtons(AdditionalButtons ButtonsKeyCode)
    {
        if (ButtonsKeyCode == AdditionalButtons.None) return true;
        if (ButtonsKeyCode == AdditionalButtons.Shift && Event.current.shift) return true;
        if (ButtonsKeyCode == AdditionalButtons.Alt && Event.current.alt) return true;
        if (ButtonsKeyCode == AdditionalButtons.Control && Event.current.control) return true;
        return false;
    }
    #endregion

    [PreferenceItem("Nodify")]
    public static void OnPreferencesGUI()
    {
        PreferencesScrollView = GUILayout.BeginScrollView(PreferencesScrollView);
        OnDrawOptions();

        ShowHotKeysSettings = GUILayout.Toggle(ShowHotKeysSettings, "HotKeys Settings", "button");

        if (ShowHotKeysSettings)
        {
            OnDrawHotKeySettings();
        }

        GUILayout.EndScrollView();
    }

    private static void OnDrawOptions()
    {
        bool showNodes = EditorGUILayout.Toggle("Show Nodes in Hierarchy", EditorPrefs.GetBool("nodify.show_nodes_in_hierarchy", false));
		bool showNodeGroups = EditorGUILayout.Toggle("Show Node Groups in Hierarchy", EditorPrefs.GetBool("nodify.show_node_groups_in_hierarchy", false));
        bool showAnchor = EditorGUILayout.Toggle("Show Anchors in Hierarchy", EditorPrefs.GetBool("nodify.show_anchors_in_hierarchy", false));
        bool automaticWindowRepaint = EditorGUILayout.Toggle("Constant Repaint Editor", EditorPrefs.GetBool("nodify.automatic_window_repaint", false));

        if (GUI.changed)
        {
            EditorPrefs.SetBool("nodify.show_anchors_in_hierarchy", showAnchor);
            EditorPrefs.SetBool("nodify.show_nodes_in_hierarchy", showNodes);
			EditorPrefs.SetBool("nodify.show_node_groups_in_hierarchy", showNodeGroups);
            EditorPrefs.SetBool("nodify.automatic_window_repaint", automaticWindowRepaint);

            SceneView.RepaintAll();
        }
    }

    private static void OnDrawHotKeySettings()
    {
        AdditionalButtons multiSelectNodes = (AdditionalButtons)EditorPrefs.GetInt("nodify.hotkeys.multi_select_nodes", 303);
        AdditionalButtons FocusOnSelectedNode = (AdditionalButtons)EditorPrefs.GetInt("nodify.hotkeys.focus_on_selected_node", 0);
        KeyCode FocusOnSelectedNode2 = (KeyCode)EditorPrefs.GetInt("nodify.hotkeys.focus_on_selected_node2", 102);
        AdditionalButtonsNotNone DeselectAll = (AdditionalButtonsNotNone)EditorPrefs.GetInt("nodify.hotkeys.deselect_all", 303);
        AdditionalButtonsNotNone BringUpAnchorsMenu = (AdditionalButtonsNotNone)EditorPrefs.GetInt("nodify.hotkeys.bring_up_anchors_menu", 303);
        AdditionalButtonsNotNone MultipleSelectAndDeselect = (AdditionalButtonsNotNone)EditorPrefs.GetInt("nodify.hotkeys.multiple_select_and_deselect", 303);
        AdditionalButtonsNotNone AutoCreateDefaultMethodAnchorForNode = (AdditionalButtonsNotNone)EditorPrefs.GetInt("nodify.hotkeys.auto_create_default_method_anchor_dor_node", 303);

        GUILayout.BeginHorizontal();
        multiSelectNodes = (AdditionalButtons)EditorGUILayout.EnumPopup("Multi-Select nodes", multiSelectNodes);
        EditorGUILayout.Popup(0, new string[] { "Drag" });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        FocusOnSelectedNode = (AdditionalButtons)EditorGUILayout.EnumPopup("Focus on selected Node", FocusOnSelectedNode);
        FocusOnSelectedNode2 = (KeyCode)EditorGUILayout.EnumPopup(FocusOnSelectedNode2);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        DeselectAll = (AdditionalButtonsNotNone)EditorGUILayout.EnumPopup("Deselect all", DeselectAll);
        EditorGUILayout.Popup(0, new string[] { "Click on Window" });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        BringUpAnchorsMenu = (AdditionalButtonsNotNone)EditorGUILayout.EnumPopup("Bring up anchors menu", BringUpAnchorsMenu);
        EditorGUILayout.Popup(0, new string[] { "Right-Click on Anchors" });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        MultipleSelectAndDeselect = (AdditionalButtonsNotNone)EditorGUILayout.EnumPopup("Multiple select & deselect", MultipleSelectAndDeselect);
        EditorGUILayout.Popup(0, new string[] { "Click on Nodes" });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        AutoCreateDefaultMethodAnchorForNode = (AdditionalButtonsNotNone)EditorGUILayout.EnumPopup("Auto-Create default method anchor for node", AutoCreateDefaultMethodAnchorForNode);
        EditorGUILayout.Popup(0, new string[] { "Right-Click on Nodes" });
        GUILayout.EndHorizontal();

        GUI.enabled = false;
        EditorGUILayout.Popup("Zoom in / out on the editor", 0, new string[] { "Scroll Wheel" });
        EditorGUILayout.Popup("Pans the editor window", 0, new string[] { "Mouse Drag on Window" });
        EditorGUILayout.Popup("Bring up the create context menu", 0, new string[] { "Right-Click on Window" });
        EditorGUILayout.Popup("Bring up node options menu", 0, new string[] { "Right-Click on Nodes" });
        EditorGUILayout.Popup("Create a new anchor connection", 0, new string[] { "Right-Click on Anchors" });
        GUI.enabled = true;

        if (GUI.changed)
        {
            EditorPrefs.SetInt("nodify.hotkeys.multi_select_nodes", (int)multiSelectNodes);

            EditorPrefs.SetInt("nodify.hotkeys.focus_on_selected_node", (int)FocusOnSelectedNode);
            EditorPrefs.SetInt("nodify.hotkeys.focus_on_selected_node2", (int)FocusOnSelectedNode2);

            EditorPrefs.SetInt("nodify.hotkeys.deselect_all", (int)DeselectAll);

            EditorPrefs.SetInt("nodify.hotkeys.bring_up_anchors_menu", (int)BringUpAnchorsMenu);

            EditorPrefs.SetInt("nodify.hotkeys.multiple_select_and_deselect", (int)MultipleSelectAndDeselect);
            EditorPrefs.SetInt("nodify.hotkeys.auto_create_default_method_anchor_dor_node", (int)AutoCreateDefaultMethodAnchorForNode);

            SceneView.RepaintAll();
        }
    }
}
