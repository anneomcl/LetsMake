using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Time/Set/TimeScale", "Time.SetTimeScale" )]
	public class UnityTimeSetTimeScale : Node 
	{
		[Expose]
		public float timeScale;
		
		protected override void OnExecute()
		{
			Time.timeScale = timeScale;
			base.OnExecute();
		}
	}
}