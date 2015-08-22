using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Get/Is Kinematic", "Rigidbody.GetIsKinematic" )]
	public class UnityRigidbodyGetIsKinematic : Node 
	{
		[Expose]
		public Rigidbody target;
		
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
