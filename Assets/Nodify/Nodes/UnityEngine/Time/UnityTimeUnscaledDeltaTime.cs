#if UNITY_4_6 || UNITY_5
using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Time/UnscaledDeltaTime", "Time.UnscaledDeltaTime" )]
	public class UnityTimeUnscaledDeltaTime : Node
	{
		[Expose]
		public float unscaledDeltaTime;
		
		private void Update()
		{
			unscaledDeltaTime = Time.unscaledDeltaTime;
		}
	}
}
#endif
