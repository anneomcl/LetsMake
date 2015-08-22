using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Vector3/Scale", "Vector3.Scale" )]
	public class UnityVector3Scale : Node 
	{
		[Expose]
		public Vector3 source;

		[Expose]
		public float scale;

		[Expose]
		public Vector3 result;

		protected override void OnExecute()
		{
			result = source * scale;
			base.OnExecute();
		}
	}
}
