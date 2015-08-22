using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/TransformDirection", "Transform.TransformDirection" )]
	public class UnityTransformTransformDirection : Node 
	{
		[Expose]
		public Transform target;

		[Expose]
		public Vector3 direction;

		[Expose]
		public Vector3 result;

		protected override void OnExecute()
		{
			if(target != null)
			{
				result = target.TransformDirection(direction);
			}
			base.OnExecute();
		}
	}
}
