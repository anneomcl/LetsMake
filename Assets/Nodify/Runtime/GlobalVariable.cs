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
using Nodify.Runtime;

namespace Nodify.Runtime
{
    [CustomRenderer("Nodify.Rendering.GlobalVariableRenderer")]
	public class GlobalVariable<T> : GlobalVariableBase
	{
        [Expose]
        public T value;

        public override object OnGetValue()
        {
            return (object)this.value;
        }

        public override void OnSetValue(object value)
        {
            this.value = (T)value;
        }
	}

    /// <summary>
    /// For NodeRenderer purposes.
    /// </summary>
    public class GlobalVariableBase : Node 
    {
        private static Dictionary<string, List<GlobalVariableBase>> variableReferences = new Dictionary<string, List<GlobalVariableBase>>();

        [Expose]
        [Tooltip("If assigned, will be accessable via the GlobalVariable.Get node in any node group.")]
        public string keyName;

        [Tooltip("If this node is disabled or destroyed, it will remove the key from the global varaibles list.")]
        public bool removeOnDisable = true;

        public object internalObjectValue;

        private void Awake()
        {
            this.OnEnable();
        }

        private void OnEnable()
        {
            if(!variableReferences.ContainsKey(keyName))
            {
                variableReferences.Add(keyName, new List<GlobalVariableBase>());
                variableReferences[keyName].Add(this);
            }
            else
            {
                if(!variableReferences[keyName].Contains(this))
                {
                    variableReferences[keyName].Add(this);
                }
            }
        }

        private void OnDisable()
        {
            if (removeOnDisable)
            {
                if (variableReferences.ContainsKey(keyName))
                {
                    variableReferences[keyName].Remove(this);
                }
            }
        }

        private void OnDestroy()
        {
            if (variableReferences.ContainsKey(keyName))
            {
                variableReferences[keyName].Remove(this);
            }
        }

        public virtual object OnGetValue()
        {
            return internalObjectValue;
        }

        public virtual void OnSetValue(object value)
        {
            internalObjectValue = value;
        }

        /// <summary>
        /// Gets the object value by keyName.
        /// A GlobalVariable must exist for a value to be 
        /// returned.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="throwOnNullKey"></param>
        /// <returns></returns>
        public static object GetValue(string keyName, bool throwOnNullKey = true)
        {
            if(variableReferences.ContainsKey(keyName))
            {
                if(variableReferences[keyName].Count > 0)
                {
                    return variableReferences[keyName][0].OnGetValue();
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                if(throwOnNullKey)
                {
                    throw new System.Exception("GlobalVariable: Key does not exist! " + keyName);
                }
            }

            return null;
        }

        /// <summary>
        /// Set the value of any GlobalVariable nodes
        /// with the same keyName.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public static void SetValue(string keyName, object value)
        {
            if(!string.IsNullOrEmpty(keyName) && variableReferences.ContainsKey(keyName))
            {
                for (int i = 0; i < variableReferences[keyName].Count; i++ )
                {
                    variableReferences[keyName][i].OnSetValue(value);
                }
            }
        }

        public override void OnFieldValueUpdated(System.Reflection.FieldInfo field)
        {
            if(field.Name == "value")
            {
                SetValue(keyName, field.GetValue(this));
            }
        }
    }
}
