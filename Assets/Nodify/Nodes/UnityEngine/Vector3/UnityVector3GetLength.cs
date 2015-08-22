using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Vector3/Get/Length", "Vector3.GetLength" )]
	public class UnityVector3GetLength : Node 
	{
		[Expose]
		public Vector3 target;

		[Expose]
		public float result;

		protected override void OnExecute()
		{
			result = target.magnitude;
			base.OnExecute();
		}
	}
}
