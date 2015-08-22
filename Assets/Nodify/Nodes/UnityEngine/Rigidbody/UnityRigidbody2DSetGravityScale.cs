using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Set/Gravity Scale", "Rigidbody2D.SetGravityScale" )]
	public class UnityRigidbody2DSetGravityScale : Node 
	{
		[Expose]
		public Rigidbody2D target;
		
		[Expose]
		public float gravityScale;

		protected override void OnExecute()
		{
			target.gravityScale = gravityScale;
			base.OnExecute();
		}
	}
}
