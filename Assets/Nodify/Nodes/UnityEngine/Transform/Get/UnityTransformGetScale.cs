using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Get/Scale", "Transform.GetScale" )]
	public class UnityTransformGetScale : Node 
	{
		[Expose]
		public Transform target;

		[Expose]
		public Vector3 result;

		protected override void OnExecute()
		{
			if(target != null)
			{
				result = target.localScale;
			}
			base.OnExecute();
		}
	}
}
