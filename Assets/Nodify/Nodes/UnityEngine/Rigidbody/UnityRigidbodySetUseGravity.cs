using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Set/Use Gravity", "Rigidbody.SetUseGravity" )]
	public class UnityRigidbodySetUseGravity : Node 
	{
		[Expose]
		public Rigidbody target;
		
		[Expose]
		public bool useGravity;

		protected override void OnExecute()
		{
			target.useGravity = useGravity;
			base.OnExecute();
		}
	}
}
