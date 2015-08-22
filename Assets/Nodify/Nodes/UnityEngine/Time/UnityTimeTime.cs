using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Time/Time", "Time.Time" )]
	public class UnityTimeTime : Node 
	{
		[Expose]
		public float time;

		private void Update()
		{
			time = Time.time;
		}
	}
}
