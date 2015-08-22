using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Collider/Collision2D", "Collider.Collision2D", "Icons/unity_collision_icon")]
	public class UnityCollision2D : Node 
	{
		[Expose]
		public GameObject target;

		[Expose]
		public Collision2D collision;

		private ManagedBehaviour m_target;

		private void Awake()
		{
            this.Execute();
		}

        protected override void OnExecute()
        {
            m_target = (target.GetComponent<ManagedBehaviour>()) ? target.GetComponent<ManagedBehaviour>() : target.AddComponent<ManagedBehaviour>();

            m_target.CollisionEntered2D += delegate(Collision2D arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidEnter");
            };

            m_target.CollisionStayed2D += delegate(Collision2D arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidStay");
            };

            m_target.CollisionExited2D += delegate(Collision2D arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidLeave");
            };
        }

        [Expose(true)]
		public void OnCollisionDidEnter(){}

		[Expose]
		public void OnCollisionDidLeave(){}

		[Expose]
		public void OnCollisionDidStay(){}
	}
}
