using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Input/Get Button Up", "Input.GetButtonUp" )]
	public class UnityInputGetButtonUp : Node 
	{
		[Expose]
		public string buttonName;
		
		[Expose(true)]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }
		
		protected override void OnExecute()
		{
			if(Input.GetButtonUp(buttonName))
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");
			
			base.OnExecute();
		}
	}
}
