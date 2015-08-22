using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Network/Instantiate", "Network.Instantiate" )]
	public class UnityNetworkInstantiate : Node 
	{
        [Expose]
        public GameObject prefab;

        [Expose]
        public Vector3 position;

        [Expose]
        public Quaternion rotation = Quaternion.identity;

        [Expose]
        public int groupID;

		protected override void OnExecute()
		{
            Network.Instantiate(prefab, position, rotation, groupID);

			base.OnExecute();
		}
	}
}
