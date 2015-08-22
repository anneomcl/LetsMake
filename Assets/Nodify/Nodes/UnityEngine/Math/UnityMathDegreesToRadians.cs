using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Degrees To Radians", "Math/Deg2Rad" )]
	public class UnityMathDegreesToRadians : Node 
	{
		[Expose]
		public float angle;
		
		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = Mathf.Deg2Rad * angle;
			base.OnExecute();
		}
	}
}
