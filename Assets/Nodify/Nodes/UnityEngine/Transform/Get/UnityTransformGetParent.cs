using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Get/Parent", "Transform.GetParent" )]
	public class UnityTransformGetParent : Node 
	{
		[Expose]
		public Transform source;

		[Expose]
		public Transform resultParent;

		[Expose]
		public string parentName = "";

		[Expose]
		public void IsNull()
		{
		}
		
		[Expose]
		public void IsNotNull()
		{
		}

		protected override void OnExecute()
		{
			if(source != null)
				resultParent = source.parent;

			if(resultParent != null)
			{
				parentName = resultParent.name;
				this.Fire("IsNotNull");
			}
			else
			{
				this.Fire("IsNull");
			}

			base.OnExecute();
		}
	}
}
