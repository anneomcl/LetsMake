using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Network/Client/Connect", "Network.Connect", "Icons/unity_network_icon" )]
	public class UnityNetworkConnect : Node 
	{
        [Expose]
        public string ipAddress = string.Empty;

        [Expose]
        public int remotePort;

		protected override void OnExecute()
		{
            if (!Network.isClient)
            {
                Network.Connect(ipAddress, remotePort);
            }

			base.OnExecute();
		}

        [Expose]
        public void ConnectedToServer()
        {
            
        }

        private void OnConnectedToServer()
        {
            this.Fire("ConnectedToServer");
        }
	}
}
