using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/LookAt", "Transform.LookAt")]
	public class UnityTransformLookAt : Node 
	{
        [Expose]
        public Transform target;

        [Expose]
        public Transform lookPoint;

        [Expose]
        public float speed;


		protected override void OnExecute()
		{
            target.rotation = Quaternion.Slerp(target.rotation, Quaternion.LookRotation(lookPoint.position - target.position), Time.deltaTime * speed);

			base.OnExecute();
		}
	}
}
