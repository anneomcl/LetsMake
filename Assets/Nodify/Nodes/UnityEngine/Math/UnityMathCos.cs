using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Cos", "Math.Cos" )]
	public class UnityMathCos : Node 
	{
		[Expose]
		public float angle;
		
		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = Mathf.Cos(angle);
			base.OnExecute();
		}
	}
}
