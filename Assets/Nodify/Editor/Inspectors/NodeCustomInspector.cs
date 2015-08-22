﻿/**
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
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Editor
{
    [CustomEditor(typeof(Node), true)]
    public class NodeCustomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Node node = target as Node;
            CreateMenu menuItem = (CreateMenu)node.GetType().GetCustomAttributes(typeof(CreateMenu), false)[0];

            base.OnInspectorGUI();
            node.OnCustomInspectorGUI();

            if (menuItem.description != null)
            {
                GUILayout.Label(new GUIContent("Description: " + menuItem.description), EditorStyles.boldLabel);
            }

            if(Application.isPlaying)
            {
                if(GUILayout.Button(new GUIContent("Execute", "Will fire this node manually. This is great for testing."), GUILayout.Height(22.0f)))
                {
                    node.Execute();
                }
            }
        }
    }

}