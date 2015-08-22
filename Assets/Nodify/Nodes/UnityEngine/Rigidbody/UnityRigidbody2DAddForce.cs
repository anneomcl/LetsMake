using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Add Force", "Rigidbody2D.AddForce")]
	public class UnityRigidbody2DAddForce : Node 
	{
        [Expose]
        public Rigidbody2D rigidbodyTarget;

        [Expose]
        public Vector2 force;

        [Expose]
        public ForceMode2D forceMode = ForceMode2D.Force;

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
