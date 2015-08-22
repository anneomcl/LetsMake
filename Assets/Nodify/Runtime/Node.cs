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
	public class Node : MonoBehaviour
    {
        #region Private Fields
        private List<FieldInfo> m_exposedFields;
		private List<FieldInfo> m_exposedGlobals;
		private List<MethodInfo> m_exposedMethods;
        private List<Expose> m_exposed;
        private float m_editorStateResetTime = .75f;
        private float m_editorTimeDelta;
        #endregion

        #region Public Fields
        /// <summary>
        /// The displayed position of this node in the group.
        /// </summary>
        [HideInInspector]
		public Vector2 editorPosition;
        /// <summary>
        /// The visible: "execution" state of the node.
        /// </summary>
        [HideInInspector]
        [System.NonSerialized]
        public ExecutionState editorExecutionState = ExecutionState.IDLE;
        /// <summary>
        /// The path of the icon for this node.
        /// </summary>
        [HideInInspector]
        public string editorResourceIcon;
        #endregion

        #region Default Exposed Methods
        [Expose(false, "Icons/node_oncomplete_icon")]
        public void OnComplete()
        {
            
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// The NodeGroup that contains this node.
        /// </summary>
        public NodeGroup parent
		{
			get
			{
				if(transform.parent != null)
				{
					return transform.parent.GetComponent<NodeGroup>();
				}

				return null;
			}
		}

        /// <summary>
        /// All exposed anchors for this node.
        /// </summary>
		public List<Anchor> anchors
		{
			get
			{
				List<Anchor> anchors = new List<Anchor>();

				foreach(Transform child in transform)
				{	
					if(child.GetComponent<Anchor>())
					{
						anchors.Add(child.GetComponent<Anchor>());
					}
				}

				return anchors;
			}
		}
        #endregion

        #region Reflection Methods
        /// <summary>
        /// Returns a List of all exposables owned
        /// by this node.
        /// </summary>
        /// <returns></returns>
        public List<Expose> GetExposed()
        {
            if(m_exposed == null)
            {
                m_exposed = new List<Expose>();

                foreach(MethodInfo info in GetType().GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
                {
                    if(info.GetCustomAttributes(typeof(Expose), true).Length > 0)
                    {
                        Expose expose = (Expose)info.GetCustomAttributes(typeof(Expose), true)[0];
                        expose.exposedType = ExposedType.METHOD;
                        expose.methodInfo = info;
                        expose.exposedName = info.Name;

                        m_exposed.Add(expose);
                    }
                }

                foreach (FieldInfo info in GetType().GetFields(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
                {
                    if (info.GetCustomAttributes(typeof(Expose), true).Length > 0)
                    {
                        Expose expose = (Expose)info.GetCustomAttributes(typeof(Expose), true)[0];
                        expose.exposedType = ExposedType.FIELD;
                        expose.fieldInfo = info;
                        expose.exposedName = info.Name;

                        m_exposed.Add(expose);
                    }
                }

                foreach (PropertyInfo info in GetType().GetProperties(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
                {
                    if (info.GetCustomAttributes(typeof(Expose), true).Length > 0)
                    {
                        Expose expose = (Expose)info.GetCustomAttributes(typeof(Expose), true)[0];
                        expose.exposedType = ExposedType.PROPERTY;
                        expose.propertyInfo = info;
                        expose.exposedName = info.Name;

                        m_exposed.Add(expose);
                    }
                }
            }

            return m_exposed;
        }

        public virtual void SetFieldValue(FieldInfo field, object value)
        {
            field.SetValue(this, value);

            this.OnFieldValueUpdated(field);
        }

        /// <summary>
        /// An overideable method for hooking into when a field has been updated.
        /// </summary>
        /// <param name="field"></param>
        public virtual void OnFieldValueUpdated(FieldInfo field)
        {

        }

        #endregion

        #region Methods
        public void Execute()
        {
        	try
        	{
	            this.OnExecute();
            }
            catch(System.Exception exception)
            {
                this.editorExecutionState = ExecutionState.ERROR;

            	Debug.LogException(exception);
            }
        }

        protected virtual void OnExecute()
        {
            this.Fire("OnComplete");
        }

        public void EditorUpdate()
        {
            this.m_editorTimeDelta += Time.deltaTime;
            
            if(this.m_editorTimeDelta >= this.m_editorStateResetTime)
            {
                this.m_editorTimeDelta = 0;

                if(editorExecutionState != ExecutionState.ERROR)
                {
                    editorExecutionState = ExecutionState.IDLE;
                }
            }

            this.OnShowHideInHierarchy();
            this.OnEditorUpdate();

            foreach(Anchor anchor in anchors)
            {
                anchor.EditorUpdate();
            }
        }

        /// <summary>
        /// Shows or Hides the node in the hierarchy based on the editor prefs setting.
        /// </summary>
        protected virtual void OnShowHideInHierarchy()
        {
            #if UNITY_EDITOR
                if (UnityEditor.EditorPrefs.GetBool("nodify.show_nodes_in_hierarchy") == false)
                {
                    gameObject.hideFlags = HideFlags.HideInHierarchy;
                }
                else
                {
                    gameObject.hideFlags = HideFlags.None;
                }
            #endif
        }

        /// <summary>
        /// Allows the user to hook into the editor update callback.
        /// </summary>
        protected virtual void OnEditorUpdate()
        {

        }

        #if UNITY_EDITOR
        /// <summary>
        /// Allos the user to hook into custom rendering for the inspector.
        /// </summary>
        public virtual void OnCustomInspectorGUI() 
        {

        }
        #endif

        /// <summary>
        /// A hook that is called after
        /// the editor creates the node. This
        /// is useful for adding additional interaction
        /// to your custom nodes.
        /// </summary>
        public virtual void OnEditorNodeCreated()
        {

        }

        /// <summary>
        /// Called before the node is executed, it forces
        /// all connected "importing" anchors to send
        /// their values in.
        /// </summary>
        public virtual void OnImportConnectedVariableAnchors()
        {

        }

        public void FireExposed(Expose exposed, params object[] args)
        {
            if(exposed.exposedType == ExposedType.METHOD)
            {
                exposed.methodInfo.Invoke(this, args);

                foreach(Anchor anchor in anchors)
                {
                    if(anchor.type == AnchorType.VARIABLE)
                    {
                        anchor.OnPrepareVariableForExport();
                        anchor.ExportVariableToConnections();
                    }
                }

                foreach(Anchor anchor in anchors)
                {
                    if(anchor.displayName == exposed.exposedName)
                    {
                        anchor.ExecuteConnectedNodes();
                    }
                }
            }

            if(exposed.exposedType == ExposedType.FIELD || exposed.exposedType == ExposedType.PROPERTY)
            {
                foreach(Anchor anchor in anchors)
                {
                    if(anchor.type == AnchorType.VARIABLE && anchor.displayName == exposed.exposedName)
                    {
                        anchor.ExecuteConnectedNodes();
                    }
                }
            }
        }

        /// <summary>
        /// Finds an exposed element by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Expose GetExposedByName(string name)
        {
            foreach(Expose exposed in GetExposed())
            {
                if(exposed.exposedName == name)
                {
                    return exposed;
                }
            }

            return null;
        }

        /// <summary>
        /// Fires the exposed method by name.
        /// </summary>
        /// <param name="methodName"></param>
        public void Fire(string methodName)
        {
            this.Fire(methodName, null);
        }

        /// <summary>
        /// Invokes the exposed method by name, if successful 
        /// will invoke all nodes connected via the anchor by the same
        /// name.
        /// </summary>
        /// <param name="methodName"></param>
        public void Fire(string methodName, params object[] args)
        {
			try
        	{
                foreach(Expose exposed in GetExposed())
                {
                    if(exposed.exposedName == methodName)
                    {
                        FireExposed(exposed);
                    }
                }

			}
			catch(System.Exception exception)
            {
                this.editorExecutionState = ExecutionState.ERROR;

            	Debug.LogException(exception);
            }
        }

        #endregion
    }

    /// <summary>
    /// The visual editor state of the node.
    /// </summary>
    public enum ExecutionState
    {
        IDLE,
        SUCCESS,
        ERROR
    }
}
