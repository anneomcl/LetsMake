using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Set/Is Kinematic", "Rigidbody2D.SetIsKinematic" )]
	public class UnityRigidbody2DSetIsKinematic : Node 
	{
		[Expose]
		public Rigidbody2D target;

		[Expose]
		public bool isKinematic;

		protected override void OnExecute()
		{
			target.isKinematic = isKinematic;
			base.OnExecute();
		}
	}
}
