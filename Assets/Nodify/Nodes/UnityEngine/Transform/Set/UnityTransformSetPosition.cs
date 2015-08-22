using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Set/Position", "Transform.SetPosition" )]
	public class UnityTransformSetPosition : Node 
	{
        [Expose]
        public Transform target;
        [Expose]
        public Vector3 position;

		protected override void OnExecute()
		{
            if(target != null)
            {
                target.position = position;
            }

			base.OnExecute();
		}
	}
}
