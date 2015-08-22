using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Set/Scale", "Transform.SetScale" )]
	public class UnityTransformSetScale : Node 
	{
		[Expose]
		public Transform target;

		[Expose]
		public Vector3 scale;

		protected override void OnExecute()
		{
			if(target != null)
			{
				target.localScale = scale;
			}
			base.OnExecute();
		}
	}
}
