using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Platform/Is Standalone", "Platform.IsStandalone?", "Icons/unity_platform_desktop_icon")]
	public class UnityPlatformIsDesktop : Node 
	{
		protected override void OnExecute()
		{
            #if UNITY_STANDALONE
                base.OnExecute();
            #endif
		}
	}
}
