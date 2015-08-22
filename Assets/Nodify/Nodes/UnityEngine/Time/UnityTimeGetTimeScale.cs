using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Time/Get/TimeScale", "Time.GetTimeScale" )]
	public class UnityTimeGetTimeScale : Node 
	{
		[Expose]
		public float timeScale;
		
		protected override void OnExecute()
		{
			timeScale = Time.timeScale;
			base.OnExecute();
		}
	}
}