using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Divide/Integer", "Math.DivideInt" )]
	public class UnityMathDivideInteger : Node 
	{
		[Expose]
		public int value1;
		
		[Expose]
		public int value2;
		
		[Expose]
		[Tooltip("In case of a divide by zero the result will be zero")]
		public int result;

		protected override void OnExecute()
		{
			result = value2 != 0 ? value1 / value2 : 0;
			base.OnExecute();
		}
	}
}
