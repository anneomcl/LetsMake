using UnityEngine;
using System;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Nodify/Event Messenger/Fire", "EventMessenger.Fire", "Icons/event_messenger_fire_icon")]
	public class NodifyEventMessengerFire : Node 
	{
        [Expose]
        public string eventName;

        [Expose]
        public EventArgs args;

		protected override void OnExecute()
		{
            EventMessenger.Fire(eventName, args);

			base.OnExecute();
		}
	}
}
