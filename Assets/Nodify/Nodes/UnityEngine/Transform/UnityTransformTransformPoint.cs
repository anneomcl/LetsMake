using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/TransformPoint", "Transform.TransformPoint" )]
	public class UnityTransformTransformPoint : Node 
	{
		[Expose]
		public Transform target;
		
		[Expose]
		public Vector3 position;
		
		[Expose]
		public Vector3 result;

		protected override void OnExecute()
		{
			if(target != null)
			{
				result = target.TransformPoint(position);
			}
			base.OnExecute();
		}
	}
}
