using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Follow", "Transform.Follow")]
	public class UnityTransformFollow : Node 
	{
        [Expose]
        public Transform target;

        [Expose]
        public Transform followPoint;

        [Expose]
        public float speed;

        [Expose]
        public Vector3 relativeOffset;

		protected override void OnExecute()
		{
            target.position = Vector3.Lerp(target.position, followPoint.position + followPoint.TransformDirection(relativeOffset), Time.deltaTime * speed);

			base.OnExecute();
		}
	}
}
