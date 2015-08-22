using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/Color", "Color")]
	public class UnityVariablesColor : GlobalVariable<Color> 
    {
        [Expose]
        [HideInInspector]
        public float r;
        [Expose]
        [HideInInspector]
        public float g;
        [Expose]
        [HideInInspector]
        public float b;
        [Expose]
        [HideInInspector]
        public float a;

        private void Update()
        {
            r = value.r;
            g = value.g;
            b = value.b;
            a = value.a;
        }

        protected override void OnEditorUpdate()
        {
            this.Update();
        }
    }
}
