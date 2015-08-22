using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Set/Is Kinematic", "Rigidbody.SetIsKinematic" )]
	public class UnityRigidbodySetIsKinematic : Node 
	{
		[Expose]
		public Rigidbody target;
		
		[Expose]
		public bool isKinematic;

		protected override void OnExecute()
		{
			target.isKinematic = isKinematic;
			base.OnExecute();
		}
	}
}
