using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Collider/Trigger", "Collider.Trigger", "Icons/unity_trigger_icon")]
    public class UnityTrigger : Node
    {
        [Expose]
        public GameObject target;

        [Expose]
        public Collider otherCollider;

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

            m_target.TriggerEntered += delegate(Collider arg)
            {
				if(tagMask.Count == 0 || tagMask.Contains(arg.tag))
				{
	                otherCollider = arg;
	                this.Fire("OnTriggerDidEnter");
				}
            };

            m_target.TriggerStayed += delegate(Collider arg)
            {
				if(tagMask.Count == 0 || tagMask.Contains(arg.tag))
				{
	                otherCollider = arg;
	                this.Fire("OnTriggerDidStay");
				}
            };
            m_target.TriggerExited += delegate(Collider arg)
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
