using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/Vector2", "Vector2")]
	public class UnityVariablesVector2 : GlobalVariable<Vector2>
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

    }
}
