using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody/Add Force", "Rigidbody.AddForce")]
	public class UnityRigidbodyAddForce : Node 
	{
        [Expose]
        public Rigidbody rigidbodyTarget;

        [Expose]
        public Vector3 force;

        [Expose]
        public ForceMode forceMode = ForceMode.Force;

        [Expose]
        public bool relative = false;

		protected override void OnExecute()
		{
            if(relative)
            {
            	rigidbodyTarget.AddForce(rigidbodyTarget.transform.TransformDirection(force), forceMode);
            }
            else
            {
                rigidbodyTarget.AddForce(force, forceMode);
            }

			base.OnExecute();
		}
	}
}
