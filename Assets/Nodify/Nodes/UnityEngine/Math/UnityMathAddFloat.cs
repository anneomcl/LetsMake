using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Add/Float", "Math.AddFloat" )]
	public class UnityMathAddFloat : Node 
	{
		[Expose]
		public float value1;

		[Expose]
		public float value2;

		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = value1 + value2;
			base.OnExecute();
		}
	}
}
