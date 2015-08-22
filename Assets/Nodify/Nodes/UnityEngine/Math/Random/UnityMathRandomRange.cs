using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Random/Range", "Math.RandomRange" )]
	public class UnityMathRandomRange : Node 
	{
		[Expose]
		[Tooltip("Inclusive.")]
		public int min;

		[Expose]
		[Tooltip("Exclusive.")]
		public int max;

		[Expose]
		[HideInInspector]
		public int result;

		protected override void OnExecute()
		{
			result = UnityEngine.Random.Range(min, max);
			base.OnExecute();
		}
	}
}
