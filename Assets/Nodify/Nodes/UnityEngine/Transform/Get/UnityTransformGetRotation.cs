using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Get/Rotation", "Transform.GetRotation" )]
	public class UnityTransformGetRotation : Node 
	{
        [Expose]
        public Transform target;
        [Expose]
        public Quaternion rotation;

        protected override void OnExecute()
        {
            if (target != null)
            {
                rotation = target.rotation;
            }

            base.OnExecute();
        }
	}
}
