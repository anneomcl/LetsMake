using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Vector3/Get/Distance", "Vector3.GetDistance" )]
	public class UnityVector3GetDistance : Node 
	{
		[Expose]
		public Vector3 value1;

		[Expose]
		public Vector3 value2;
		
		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = Vector3.Distance(value1, value2);
			base.OnExecute();
		}
	}
}
