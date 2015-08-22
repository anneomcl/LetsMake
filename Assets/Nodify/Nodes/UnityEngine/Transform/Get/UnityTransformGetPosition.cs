using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Get/Position", "Transform.GetPosition" )]
	public class UnityTransformGetPosition : Node 
	{
        [Expose]
        public Transform target;
        [Expose]
        public Vector3 position;

		protected override void OnExecute()
		{
            if(target != null)
            {
                position = target.position;
            }

			base.OnExecute();
		}
	}
}
