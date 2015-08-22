using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Light/Set Intensity", "Light.SetIntensity" )]
	public class UnityLightSetIntensity : Node 
	{
		[Expose]
		public Light target;

		[Expose]
		public float intensity;

		protected override void OnExecute()
		{
			target.intensity = intensity;
			base.OnExecute();
		}
	}
}
