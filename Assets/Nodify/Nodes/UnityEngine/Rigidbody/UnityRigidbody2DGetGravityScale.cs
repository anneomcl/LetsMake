using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Get/Gravity Scale", "Rigidbody2D.GetGravityScale" )]
	public class UnityRigidbody2DGetGravityScale : Node 
	{
		[Expose]
		public Rigidbody2D target;

		[Expose]
		public float gravityScale;

		protected override void OnExecute()
		{
			gravityScale = target.gravityScale;
			base.OnExecute();
		}
	}
}
