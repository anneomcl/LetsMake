using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/Transform", "Transform", "Icons/unity_gameobject_variable_icon")]
	public class UnityVariablesTransform : GlobalVariable<Transform>
    {
        [Expose]
        public Vector3 position
        {
            get
            {
                if (this.value != null)
                {
                    return this.value.position;
                }

                return Vector3.zero;
            }
            set
            {
                if (this.value != null)
                {
                    this.value.position = value;
                }

            }
        }
    }
}
