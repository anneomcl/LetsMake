using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/Vector4", "Vector4")]
	public class UnityVariablesVector4 : GlobalVariable<Vector4> 
    {
        [Expose]
        public float x
        {
            get { return this.value.x; }
            set { this.value.x = value; }
        }

        [Expose]
        public float y
        {
            get { return this.value.y; }
            set { this.value.y = value; }
        }

        [Expose]
        public float z
        {
            get { return this.value.z; }
            set { this.value.z = value; }
        }

        [Expose]
        public float w
        {
            get { return this.value.w; }
            set { this.value.w = value; }
        }
    }
}
