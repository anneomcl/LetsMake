using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Set/Rotation", "Transform.SetRotation" )]
	public class UnityTransformSetRotation : Node 
	{
        [Expose]
        public Transform target;
        [Expose]
        public Quaternion rotation;

        protected override void OnExecute()
        {
            if (target != null)
            {
                target.rotation = rotation;
            }

            base.OnExecute();
        }
	}
}
