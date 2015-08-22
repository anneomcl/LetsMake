using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Set/Parent", "Transform.SetParent" )]
	public class UnityTransformSetParent : Node 
	{
		[Expose]
		public Transform target;
		[Expose]
		public Transform targetParent;
		[Expose]
		public bool keepWorldPosition;
		
		protected override void OnExecute()
		{
			if(target != null)
				target.SetParent(targetParent, keepWorldPosition);
			
			base.OnExecute();
		}
	}
}
