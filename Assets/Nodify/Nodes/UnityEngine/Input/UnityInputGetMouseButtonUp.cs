using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Input/Get Mouse Button Up", "Input.GetMouseButtonUp" )]
	public class UnityInputGetMouseButtonUp : Node 
	{
		[Expose]
		public int button;
		
		[Expose(true)]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }

		protected override void OnExecute()
		{
			if(Input.GetMouseButtonUp(button))
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");
			base.OnExecute();
		}
	}
}
