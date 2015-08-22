using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Platform/Is Mobile", "Platform.IsMobile?", "Icons/unity_platform_mobile_icon")]
	public class UnityPlatformIsMobile : Node 
	{
		protected override void OnExecute()
		{
            #if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8 || UNITY_BLACKBERRY
                base.OnExecute();
            #endif
		}
	}
}
