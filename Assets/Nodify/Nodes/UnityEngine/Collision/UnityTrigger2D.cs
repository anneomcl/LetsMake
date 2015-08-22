using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Collider/Trigger2D", "Collider.Trigger2D", "Icons/unity_trigger_icon")]
    public class UnityTrigger2D : Node
    {
        [Expose]
        public GameObject target;

        [Expose]
        public Collider2D otherCollider;

		public List<string> tagMask;

        private ManagedBehaviour m_target;

		[Expose(true)]
		public void OnTriggerDidEnter() { }
		
		[Expose]
		public void OnTriggerDidLeave() { }
		
		[Expose]
		public void OnTriggerDidStay() { }

        private void Awake()
        {
            m_target = (target.GetComponent<ManagedBehaviour>()) ? target.GetComponent<ManagedBehaviour>() : target.AddComponent<ManagedBehaviour>();

            m_target.TriggerEntered2D += delegate(Collider2D arg)
            {
				if(tagMask.Count == 0 || tagMask.Contains(arg.tag))
				{
	                otherCollider = arg;
	                this.Fire("OnTriggerDidEnter");
				}
            };

            m_target.TriggerStayed2D += delegate(Collider2D arg)
            {
				if(tagMask.Count == 0 || tagMask.Contains(arg.tag))
				{
	                otherCollider = arg;
	                this.Fire("OnTriggerDidStay");
				}
            };
            m_target.TriggerExited2D += delegate(Collider2D arg)
            {
				if(tagMask.Count == 0 || tagMask.Contains(arg.tag))
				{
	                otherCollider = arg;
	                this.Fire("OnTriggerDidLeave");
				}
            };
        }
    }
}
