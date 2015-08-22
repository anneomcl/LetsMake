using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Sin", "Math.Sin" )]
	public class UnityMathSin : Node 
	{
		[Expose]
		public float angle;

		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = Mathf.Sin(angle);
			base.OnExecute();
		}
	}
}
