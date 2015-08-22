using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Light/Set Range", "Light.SetRange" )]
	public class UnityLightSetRange : Node 
	{
		[Expose]
		public Light target;

		[Expose]
		public float range;

		protected override void OnExecute()
		{
			target.range = range;
			base.OnExecute();
		}
	}
}
