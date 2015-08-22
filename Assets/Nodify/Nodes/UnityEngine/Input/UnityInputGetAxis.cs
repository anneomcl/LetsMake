using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Input/Get Axis", "Input.GetAxis" )]
	public class UnityInputGetAxis : Node 
	{
		[Expose]
		public string axisName;

		[Expose]
		public float axis;

		protected override void OnExecute()
		{
			axis = Input.GetAxis(axisName);
			base.OnExecute();
		}
	}
}
