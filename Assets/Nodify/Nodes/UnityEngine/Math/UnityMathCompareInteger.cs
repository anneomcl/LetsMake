using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Compare/Integer", "Math.CompareInt" )]
	public class UnityMathCompareInteger : Node 
	{
		[Expose]
		public int value1;
		
		[Expose]
		public int value2;
		
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
			if(value1 == value2)
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
	}
}
