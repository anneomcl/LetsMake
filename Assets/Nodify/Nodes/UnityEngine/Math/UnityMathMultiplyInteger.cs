using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Multiply/Integer", "Math.MultiplyInt" )]
	public class UnityMathMultiplyInteger : Node 
	{
		[Expose]
		public int value1;
		
		[Expose]
		public int value2;
		
		[Expose]
		public int result;

		protected override void OnExecute()
		{
			result = value1 * value2;
			base.OnExecute();
		}
	}
}
