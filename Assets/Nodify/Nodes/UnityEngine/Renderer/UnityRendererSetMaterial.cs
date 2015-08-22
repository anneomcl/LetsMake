using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Renderer/Set/Material", "Renderer.SetMaterial" )]
	public class UnityRendererSetMaterial : Node 
	{
        [Expose]
        public Renderer target;
        [Expose]
        public Material material;

		protected override void OnExecute()
		{
            target.material = material;
			base.OnExecute();
		}
	}
}
