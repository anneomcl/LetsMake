using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Teleporter))]
[CanEditMultipleObjects]
public class TeleporterInspector : Editor
{
	private SerializedProperty teleportMode;
	private SerializedProperty sceneName;
	private SerializedProperty otherTeleporter;
	private SerializedProperty spawnPoint;

	private void OnEnable()
	{
		teleportMode = serializedObject.FindProperty("teleportMode");
		sceneName = serializedObject.FindProperty("sceneName");
		otherTeleporter = serializedObject.FindProperty("otherTeleporter");
		spawnPoint = serializedObject.FindProperty("spawnPoint");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(teleportMode);
		if((TeleportMode)teleportMode.enumValueIndex == TeleportMode.Scene) 
		{
			EditorGUILayout.PropertyField(sceneName);
		}
		else 
		{
			EditorGUILayout.PropertyField(otherTeleporter);
		}
		EditorGUILayout.PropertyField(spawnPoint);
		serializedObject.ApplyModifiedProperties();
	}
}