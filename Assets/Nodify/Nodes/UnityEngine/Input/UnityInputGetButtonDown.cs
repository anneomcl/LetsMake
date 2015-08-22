using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Input/Get Button Down", "Input.GetButtonDown" )]
	public class UnityInputGetButtonDown : Node 
	{
		[Expose]
		public string buttonName;
		
		[Expose(true)]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }
		
		protected override void OnExecute()
		{
			if(Input.GetButtonDown(buttonName))
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");
			
			base.OnExecute();
		}
	}
}
