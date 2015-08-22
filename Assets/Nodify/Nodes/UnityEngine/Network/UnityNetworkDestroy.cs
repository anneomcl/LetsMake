using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Network/Destroy", "Network.Destroy" )]
	public class UnityNetworkDestroy : Node 
	{
        [Expose]
        public GameObject target;

		protected override void OnExecute()
		{
            Network.Destroy(target);

			base.OnExecute();
		}
	}
}
