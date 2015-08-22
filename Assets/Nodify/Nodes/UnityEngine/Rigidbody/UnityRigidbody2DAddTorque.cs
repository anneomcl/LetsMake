using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Rigidbody2D/Add Torque", "Rigidbody2D.AddTorque")]
	public class UnityRigidbody2DAddTorque : Node 
	{
        [Expose]
        public Rigidbody2D rigidbodyTarget;

        [Expose]
        public float torque;

        [Expose]
        public ForceMode2D forceMode = ForceMode2D.Force;

		protected override void OnExecute()
		{
            rigidbodyTarget.AddTorque(torque, forceMode);

			base.OnExecute();
		}
	}
}