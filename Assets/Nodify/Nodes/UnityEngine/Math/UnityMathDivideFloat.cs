using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Divide/Float", "Math.DivideFloat" )]
	public class UnityMathDivideFloat : Node 
	{
		[Expose]
		public float value1;

		[Expose]
		public float value2;

		[Expose]
		[Tooltip("In case of a divide by zero the result will be zero")]
		public float result;

		protected override void OnExecute()
		{
			if(Mathf.Approximately(value2, 0.0f))
				result = 0.0f;
			else
				result = value1 / value1;

			base.OnExecute();
		}
	}
}
