using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Platform/Is WebPlayer", "Platform.IsWebPlayer?", "Icons/unity_platform_web_icon")]
	public class UnityPlatformIsWebPlayer : Node 
	{
		protected override void OnExecute()
		{
            #if UNITY_WEBPLAYER
                base.OnExecute();
            #endif
		}
	}
}
