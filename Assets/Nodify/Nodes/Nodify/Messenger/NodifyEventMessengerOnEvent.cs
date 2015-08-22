using UnityEngine;
using System;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Event Messenger/On Event", "EventMessenger.OnEvent", "Icons/event_messenger_on_icon")]
	public class NodifyEventMessengerOnEvent : Node 
	{
        [Expose]
        public string eventName;

        [Expose]
        public EventArgs eventArgs;

        [Expose(true)]
		public void OnEvent() { }

        private void Awake()
        {
            EventMessenger.Register(eventName, HandleOnEvent);
        }

		private void OnDestroy()
		{
			EventMessenger.Unregister(eventName, HandleOnEvent);
		}

		private void HandleOnEvent(EventArgs args)
		{
			this.eventArgs = args;
			this.Fire("OnEvent");
		}
	}
}
