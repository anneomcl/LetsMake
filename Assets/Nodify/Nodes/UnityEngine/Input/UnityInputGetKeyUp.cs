using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Input/Get Key Up", "Input.GetKeyUp" )]
	public class UnityInputGetKeyUp : Node 
	{
		[Expose]
		public KeyCode keyCode;
		
		[Expose(true)]
		public void OnTrue() { }
		
		[Expose]
		public void OnFalse() { }

		protected override void OnExecute()
		{
			if(Input.GetKeyUp(keyCode))
				this.Fire("OnTrue");
			else
				this.Fire("OnFalse");

			base.OnExecute();
		}
	}
}
