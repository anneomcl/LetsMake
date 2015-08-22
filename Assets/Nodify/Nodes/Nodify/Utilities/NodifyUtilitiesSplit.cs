using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Utilities/Split", "Nodify.Split" )]
	public class NodifyUtilitiesSplit : Node 
	{
		[Expose]
		public void Out1() { }

		[Expose]
		public void Out2() { }

		[Expose]
		public void Out3() { }

		[Expose]
		public void Out4() { }

		[Expose]
		public void Out5() { }

		protected override void OnExecute()
		{
			Fire("Out1");
			Fire("Out2");
			Fire("Out3");
			Fire("Out4");
			Fire("Out5");
			base.OnExecute();
		}
	}
}
