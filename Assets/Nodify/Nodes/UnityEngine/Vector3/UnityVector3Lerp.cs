using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Vector3/Lerp", "Vector3.Lerp" )]
	public class UnityVector3Lerp : Node 
	{
        [Expose]
        public Vector3 from;
        [Expose]
        public Vector3 to;
        [Expose]
        public Vector3 result;
        [Expose]
        public float t;

		protected override void OnExecute()
		{
            result = Vector3.Lerp(from, to, t);

			base.OnExecute();
		}
	}
}
