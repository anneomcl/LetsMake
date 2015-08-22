using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Debug/Log Warning", "Debug.LogWarning" )]
	public class UnityDebugLogWarning : Node 
	{
		[Expose]
		public string warningToLog;
		
		protected override void OnExecute()
		{
			Debug.LogWarning(warningToLog);
			base.OnExecute();
		}
	}
}
