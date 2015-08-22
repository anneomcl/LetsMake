using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Radians To Degrees", "Math.Rad2Deg" )]
	public class UnityMathRadiansToDegrees : Node 
	{
		[Expose]
		public float angle;
		
		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = Mathf.Rad2Deg * angle;
			base.OnExecute();
		}
	}
}
