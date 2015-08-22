using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/GameObject/Broadcasting/Broadcast", "GameObject.Broadcast", "Icons/unity_gameobject_broadcast_icon")]
	public class UnityGameObjectBroadcast : Node 
	{
        [Expose]
        public GameObject target;

        [Expose]
        public string methodName;

        [Expose]
        public SendMessageOptions options;

		protected override void OnExecute()
		{
            target.BroadcastMessage(methodName, options);

			base.OnExecute();
		}
	}
}
