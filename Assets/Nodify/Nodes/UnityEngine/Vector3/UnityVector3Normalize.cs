using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Vector3/Normalize", "Vector3.Normalize" )]
	public class UnityVector3Normalize : Node 
	{
		[Expose]
		public Vector3 source;
		
		[Expose]
		public Vector3 result;

		protected override void OnExecute()
		{
			result = Vector3.Normalize(source);
			base.OnExecute();
		}
	}
}
