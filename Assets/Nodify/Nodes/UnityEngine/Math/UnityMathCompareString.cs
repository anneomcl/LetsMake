using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Compare/String", "Math.CompareString" )]
	public class UnityMathCompareString : Node 
	{
		[Expose]
		public string value1;

		[Expose]
		public string value2;

		public System.StringComparison stringComparison = System.StringComparison.InvariantCultureIgnoreCase;

		[Expose]
		public void IsEqual() { }
		
		[Expose]
		public void IsNotEqual() { }
		
		[Expose]
		public void IsGreater() { }
		
		[Expose]
		public void IsLess() { }

		protected override void OnExecute()
		{
			if(value1 == null || value2 == null)
			{
				this.Fire("IsNotEqual");
				base.OnExecute();
				return;
			}

			int result = string.Compare(value1, value2, stringComparison);
			if(result == 0)
			{
				this.Fire("IsEqual");
			}
			else if(result > 0)
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
