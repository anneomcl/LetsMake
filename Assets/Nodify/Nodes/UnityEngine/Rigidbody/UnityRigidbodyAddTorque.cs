using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Add Torque", "Rigidbody.AddTorque")]
	public class UnityRigidbodyAddTorque : Node 
	{
        [Expose]
        public Rigidbody rigidbodyTarget;

        [Expose]
        public Vector3 torque;

        [Expose]
        public ForceMode forceMode = ForceMode.Force;

        [Expose]
        public bool relative = false;

		protected override void OnExecute()
		{
            if(relative)
            {
            	rigidbodyTarget.AddTorque(rigidbodyTarget.transform.TransformDirection(torque), forceMode);
            }
            else
            {
                rigidbodyTarget.AddTorque(torque, forceMode);
            }

			base.OnExecute();
		}
	}
}