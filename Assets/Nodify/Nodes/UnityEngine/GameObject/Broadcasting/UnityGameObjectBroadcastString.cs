using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/GameObject/Broadcasting/Broadcast [String]", "GameObject.Broadcast [String]", "Icons/unity_gameobject_broadcast_icon")]
	public class UnityGameObjectBroadcastString : Node 
	{
        [Expose]
        public GameObject target;

        [Expose]
        public string methodName;

        [Expose]
        public SendMessageOptions options;

        [Expose]
        public string argument;

		protected override void OnExecute()
		{
            target.BroadcastMessage(methodName, argument, options);

			base.OnExecute();
		}
	}
}
