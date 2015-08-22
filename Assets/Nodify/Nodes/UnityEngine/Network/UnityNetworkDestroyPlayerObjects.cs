using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Network/DestroyPlayerObjects", "Network.DestroyPlayerObjects" )]
	public class UnityNetworkDestroyPlayerObjects : Node 
	{
        [Expose]
        [HideInInspector]
        public NetworkPlayer player;

		protected override void OnExecute()
		{
            Network.DestroyPlayerObjects(player);

			base.OnExecute();
		}
	}
}
