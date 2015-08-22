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
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using Nodify.Runtime;

namespace Nodify.Editor
{
    [CustomEditor(typeof(Anchor), true)]
    public class NodifyAnchorCustomInspector : UnityEditor.Editor
    {
		private GUIContent m_showValueInEditorContent = null;
		private Anchor m_anchor;

		private void OnEnable()
		{
			m_anchor = (Anchor)target;
			if(m_anchor.type == AnchorType.VARIABLE)
			{
				m_showValueInEditorContent = new GUIContent("Show Value In Editor", "Should the value of the field be displayed as a string on the anchor?");
			}
		}

		public override void OnInspectorGUI()
        {
            if(m_anchor.type == AnchorType.VARIABLE)
            	DrawVariableInspector();
			else if(m_anchor.type == AnchorType.COMMENT)
            	DrawCommentInspector();
        }

		private void DrawVariableInspector()
		{
			if(m_anchor.parent == null)
				return;

			SerializedObject parent = new SerializedObject(m_anchor.parent);
			SerializedProperty property = parent.FindProperty(m_anchor.displayName);
			if(property != null)
			{
				EditorGUILayout.PropertyField(property, m_showValueInEditorContent, false);
				parent.ApplyModifiedProperties();
			}
			m_anchor.showValueInEditor = EditorGUILayout.Toggle("Show Value in Editor", m_anchor.showValueInEditor);
		}

		private void DrawCommentInspector()
		{
			SerializedProperty property = serializedObject.FindProperty("displayName");
			if(property != null)
			{
				GUILayout.Label("Comment");
				property.stringValue = EditorGUILayout.TextArea(property.stringValue, GUILayout.MinHeight(40.0f));
			}
			serializedObject.ApplyModifiedProperties();
		}
    }
}