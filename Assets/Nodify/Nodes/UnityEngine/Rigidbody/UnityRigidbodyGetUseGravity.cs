using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Get/Use Gravity", "Rigidbody.GetUseGravity" )]
	public class UnityRigidbodyGetUseGravity : Node 
	{
		[Expose]
		public Rigidbody target;

		[Expose]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }
		
		protected override void OnExecute()
		{
			if(target.useGravity)
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");
			base.OnExecute();
		}
	}
}
