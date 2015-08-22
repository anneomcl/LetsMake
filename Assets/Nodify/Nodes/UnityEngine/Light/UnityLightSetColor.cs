using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Light/Set Color", "Light.SetColor" )]
	public class UnityLightSetColor : Node 
	{
		[Expose]
		public Light target;

		[Expose]
		public Color color;

		protected override void OnExecute()
		{
			target.color = color;
			base.OnExecute();
		}
	}
}
