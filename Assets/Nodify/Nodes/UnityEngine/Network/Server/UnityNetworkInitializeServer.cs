using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Network/Server/InitializeServer", "Network.InitializeServer", "Icons/unity_network_icon" )]
	public class UnityNetworkInitializeServer : Node 
	{
        [Expose]
        public int maxConnections = 100;
        [Expose]
        public int port;
        [Expose]
        public bool useNAT = false;
        
        [Expose]
        public void ServerInitialized()
        {
            
        }

		protected override void OnExecute()
		{
            if (!Network.isServer)
            {
                Network.InitializeServer(maxConnections, port, useNAT);
            }

			base.OnExecute();
		}

        private void OnServerInitialized()
        {
            this.Fire("ServerInitialized");
        }
	}
}
