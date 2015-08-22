using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Network/Events", "Network.Events", "Icons/unity_network_icon")]
	public class UnityNetworkNetworkEvents : Node 
	{
        private ManagedNetworkBehaviour m_networkBehaviour;

        #region Exposed Fields
        [Expose]
        public GameObject target;

        [Expose]
        [HideInInspector]
        public NetworkPlayer player;

        [Expose]
        [HideInInspector]
        public NetworkConnectionError error;

        [Expose]
        [HideInInspector]
        public NetworkMessageInfo info;
        #endregion

        #region Exposed Methods
        [Expose]
        public void ServerInitialized()
        {
            
        }
        [Expose]
        public void FailedToConnect()
        {
            
        }
        [Expose]
        public void PlayerConnected()
        {
            
        }
        [Expose]
        public void PlayerDisconnected()
        {
            
        }
        [Expose]
        public void ConnectedToServer()
        {
            
        }
        [Expose]
        public void DisconnectedFromServer()
        {
            
        }
        [Expose]
        public void NetworkInstantiated()
        {
            
        }
        #endregion

        #region Event Listeners
        private void OnEnabled()
        {
            AddListeners();
        }

        private void OnDisabled()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            m_networkBehaviour.ConnectedToServer += m_networkBehaviour_ConnectedToServer;
            m_networkBehaviour.DisconnectedFromServer += m_networkBehaviour_DisconnectedFromServer;
            m_networkBehaviour.FailedToConnect += m_networkBehaviour_FailedToConnect;
            m_networkBehaviour.PlayerConnected += m_networkBehaviour_PlayerConnected;
            m_networkBehaviour.PlayerDisconnected += m_networkBehaviour_PlayerDisconnected;
            m_networkBehaviour.ServerInitialized += m_networkBehaviour_ServerInitialized;
            m_networkBehaviour.NetworkInstantiated += m_networkBehaviour_NetworkInstantiated;
        }

        private void RemoveListeners()
        {
            m_networkBehaviour.ConnectedToServer -= m_networkBehaviour_ConnectedToServer;
            m_networkBehaviour.DisconnectedFromServer -= m_networkBehaviour_DisconnectedFromServer;
            m_networkBehaviour.FailedToConnect -= m_networkBehaviour_FailedToConnect;
            m_networkBehaviour.PlayerConnected -= m_networkBehaviour_PlayerConnected;
            m_networkBehaviour.PlayerDisconnected -= m_networkBehaviour_PlayerDisconnected;
            m_networkBehaviour.ServerInitialized -= m_networkBehaviour_ServerInitialized;
            m_networkBehaviour.NetworkInstantiated -= m_networkBehaviour_NetworkInstantiated;
        }

        void m_networkBehaviour_NetworkInstantiated(NetworkMessageInfo obj)
        {
            this.info = obj;
            this.Fire("NetworkInstantiated");
        }

        void m_networkBehaviour_ServerInitialized()
        {
            this.Fire("ServerInitialized");
        }

        void m_networkBehaviour_PlayerDisconnected(NetworkPlayer obj)
        {
            this.player = obj;
            this.Fire("PlayerDisconnected");
        }

        void m_networkBehaviour_PlayerConnected(NetworkPlayer obj)
        {
            this.player = obj;
            this.Fire("PlayerConnected");
        }

        void m_networkBehaviour_FailedToConnect(NetworkConnectionError obj)
        {
            this.error = obj;
            this.Fire("FailedToConnect");
        }

        void m_networkBehaviour_DisconnectedFromServer()
        {
            this.Fire("DisconnectedFromServer");
        }

        void m_networkBehaviour_ConnectedToServer()
        {
            this.Fire("ConnectedToServer");
        }
        #endregion

        private void Awake()
        {
            if (target == null) { target = gameObject;  }

            m_networkBehaviour = (target.GetComponent<ManagedNetworkBehaviour>()) ? target.GetComponent<ManagedNetworkBehaviour>() : target.AddComponent<ManagedNetworkBehaviour>();

            AddListeners();
        }
	}
}
