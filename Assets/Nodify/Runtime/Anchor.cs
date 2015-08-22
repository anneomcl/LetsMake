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
using System.Collections.Generic;
using System.Reflection;

namespace Nodify.Runtime
{
	[ExecuteInEditMode()]
	public class Anchor : MonoBehaviour 
	{
		public string displayName;
		public Vector2 editorPosition;
		public AnchorType type = AnchorType.VARIABLE;

        /// <summary>
        /// Should the value of the field be displayed 
        /// as a string on the anchor?
        /// </summary>
        public bool showValueInEditor = false;

        private List<AnchorConnection> m_anchorConnections;
        private List<NodeConnection> m_nodeConnections;
        private Expose m_exposedReference;

        private object m_currentValue;
        private object m_lastValue;

        public Expose exposedReference
        {
        	get
        	{
        		if(parent != null && m_exposedReference == null)
        		{
                    m_exposedReference = parent.GetExposedByName(displayName);
        		}

                return m_exposedReference;
        	}
        }


		public Node parent
		{
			get
			{
				if(transform.parent != null)
				{
					return transform.parent.GetComponent<Node>();
				}

				return null;
			}
		}

		public List<AnchorConnection> anchorConnections
		{
			get
			{
                if(m_anchorConnections == null)
                {
                    m_anchorConnections = LookupAnchorConnections();
                }

                if(Application.isPlaying)
                {
                    return m_anchorConnections;
                }
                else
                {
                    return LookupAnchorConnections();
                }
			}
		}

        public List<NodeConnection> nodeConnections
        {
            get
            {
                if(m_nodeConnections == null)
                {
                    m_nodeConnections = LookupNodeConnections();
                }

                if (Application.isPlaying)
                {
                    return m_nodeConnections;
                }
                else
                {
                    return LookupNodeConnections();
                }
            }
        }

        public List<AnchorConnection> LookupAnchorConnections()
        {
            List<AnchorConnection> results = new List<AnchorConnection>();

            foreach (Transform child in transform)
            {
                AnchorConnection connection = child.GetComponent<AnchorConnection>();

                if (connection != null && connection.target != null)
                {
                    results.Add(connection);
                }
            }

            return results;
        }

        public List<NodeConnection> LookupNodeConnections()
        {
            List<NodeConnection> results = new List<NodeConnection>();

            foreach (Transform child in transform)
            {
                NodeConnection connection = child.GetComponent<NodeConnection>();

                if (connection != null && connection.target != null)
                {
                    results.Add(connection);
                }
            }

            return results;
        }

        private void Awake()
        {
            OnPrepareVariableForExport();
            ExportVariableToConnections();
        }

        private void Update()
        {
            OnPrepareVariableForExport();
            ExportVariableToConnections();
        }

        public void EditorUpdate()
        {
            #if UNITY_EDITOR
                if (UnityEditor.EditorPrefs.GetBool("nodify.show_anchors_in_hierarchy") == false)
                {
                    gameObject.hideFlags = HideFlags.HideInHierarchy;
                }
                else
                {
                    gameObject.hideFlags = HideFlags.None;
                }
            #endif
        }

        public virtual void OnPrepareVariableForExport()
        {
        	if(type == AnchorType.VARIABLE)
        	{
                if(exposedReference != null)
                {
                    if(exposedReference.exposedType == ExposedType.FIELD)
                    {
                        m_currentValue = exposedReference.fieldInfo.GetValue(parent);
                    }

                    if(exposedReference.exposedType == ExposedType.PROPERTY)
                    {
                        m_currentValue = exposedReference.propertyInfo.GetValue(parent, null);
                    }

                }
        	}
        }

        public virtual void ExportVariableToConnections()
        {
            if (type == AnchorType.VARIABLE)
            {
                if (m_currentValue != m_lastValue)
                {
                    m_lastValue = m_currentValue;

                    foreach (AnchorConnection connection in anchorConnections)
                    {
                        connection.target.OnImportVariableFromConnection(this, m_lastValue);
                    }
                }
            }
        }


        public virtual void OnImportVariableFromConnection(Anchor sender, object value)
        {
            if(exposedReference != null)
            {
                if(exposedReference.exposedType == ExposedType.FIELD)
                {
                    exposedReference.fieldInfo.SetValue(parent, value);
                }

                if(exposedReference.exposedType == ExposedType.PROPERTY)
                {
                    exposedReference.propertyInfo.SetValue(parent, value, null);
                }
            }
        }

        public void ConnectToAnchor(Anchor anchor)
        {
            GameObject cObj = new GameObject("AnchorConnection");
            AnchorConnection cScript = cObj.AddComponent<AnchorConnection>();
            cScript.target = anchor;
            cObj.transform.parent = transform;
        }

        public void ConnectToNode(Node node)
        {
            GameObject cObj = new GameObject("NodeConnection");
            NodeConnection cScript = cObj.AddComponent<NodeConnection>();
            cScript.target = node;
            cObj.transform.parent = transform;
        }

        public void RemoveConnection(AnchorConnection connection)
        {
        	if(m_anchorConnections == null) { return; }

        	m_anchorConnections.Remove(connection);
        }

		public void RemoveConnection(NodeConnection connection)
        {
        	if(m_nodeConnections == null) { return; }

        	m_nodeConnections.Remove(connection);
        }

        public void ExecuteConnectedNodes()
        {
            for(int i = 0; i < nodeConnections.Count; i++)
            {
                if (nodeConnections[i].target != null)
                {
                    nodeConnections[i].SetConnectionState(ConnectionState.RUN);
                    nodeConnections[i].target.Execute();
                }
            }
        }
	}

		
	public enum AnchorType
	{
		VARIABLE,
        COMMENT,
        METHOD
	}
}
