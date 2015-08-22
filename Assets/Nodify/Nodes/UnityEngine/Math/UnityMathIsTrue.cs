using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Math/Is True", "Math.IsTrue" )]
	public class UnityMathIsTrue : Node 
	{
		[Expose]
		public bool value;

		[Expose(true)]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }

		protected override void OnExecute()
		{
			if(value)
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");

			base.OnExecute();
		}
	}
}
