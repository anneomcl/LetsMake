using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Get/Is Kinematic", "Rigidbody2D.GetIsKinematic" )]
	public class UnityRigidbody2DGetIsKinematic : Node 
	{
		[Expose]
		public Rigidbody2D target;

		[Expose]
		public void OnTrue() { }

		[Expose]
		public void OnFalse() { }

		protected override void OnExecute()
		{
			if(target.isKinematic)
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");
			base.OnExecute();
		}
	}
}
