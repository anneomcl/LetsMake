using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Collider/Collision", "Collider.Collision", "Icons/unity_collision_icon")]
	public class UnityCollision : Node 
	{
		[Expose]
		public GameObject target;

		[Expose]
		public Collision collision;

		private ManagedBehaviour m_target;

		private void Awake()
		{
            this.Execute();
		}

        protected override void OnExecute()
        {
            m_target = (target.GetComponent<ManagedBehaviour>()) ? target.GetComponent<ManagedBehaviour>() : target.AddComponent<ManagedBehaviour>();

            m_target.CollisionEntered += delegate(Collision arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidEnter");
            };

            m_target.CollisionStayed += delegate(Collision arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidStay");
            };

            m_target.CollisionExited += delegate(Collision arg)
            {
                collision = arg;
                this.Fire("OnCollisionDidLeave");
            };
        }

        [Expose(true)]
		public void OnCollisionDidEnter()
		{

		}

		[Expose]
        public void OnCollisionDidLeave()
		{

		}

		[Expose]
		public void OnCollisionDidStay()
		{
	
		}


	}
}
