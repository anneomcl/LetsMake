using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Compare/Float", "Math.CompareFloat" )]
	public class UnityMathCompareFloat : Node 
	{
		[Expose]
		public float value1;

		[Expose]
		public float value2;

		public bool useCustomTolerance = false;

		public float customTolerance = 0.0f;

		[Expose]
		public void IsEqual() { }

		[Expose]
		public void IsNotEqual() { }

		[Expose]
		public void IsGreater() { }

		[Expose]
		public void IsGreaterOrEqual() { }

		[Expose]
		public void IsLess() { }

		[Expose]
		public void IsLessOrEqual() {}

		protected override void OnExecute()
		{
			if(AreValuesEqual())
			{
				this.Fire("IsEqual");
				this.Fire("IsGreaterOrEqual");
				this.Fire("IsLessOrEqual");
			}
			else if(value1 > value2)
			{
				this.Fire("IsGreater");
				this.Fire("IsNotEqual");
			}
			else
			{
				this.Fire("IsLess");
				this.Fire("IsNotEqual");
			}
			base.OnExecute();
		}

		private bool AreValuesEqual()
		{
			if(useCustomTolerance)
			{
				return Mathf.Abs(value1 - value2) <= customTolerance;
			}
			else
			{
				return Mathf.Approximately(value1, value2);
			}
		}
	}
}
