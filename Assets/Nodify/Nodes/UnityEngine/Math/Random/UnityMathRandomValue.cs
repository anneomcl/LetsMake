using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Random/Value", "Math.RandomValue" )]
	public class UnityMathRandomValue : Node 
	{
		[Expose]
		[HideInInspector]
		public float value;

		protected override void OnExecute()
		{
			value = UnityEngine.Random.value;
			base.OnExecute();
		}
	}
}
