using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Transform/Rotate", "Transform.Rotate")]
	public class UnityTransformRotate : Node 
	{
        [Expose]
        public Transform target;

        [Expose]
        public Vector3 axis;

        [Expose]
        public float angle;

        [Expose]
        public bool useDeltaTime;

		protected override void OnExecute()
		{
            if (target != null)
            {
                if (useDeltaTime)
                {
                    target.Rotate(axis, angle * Time.deltaTime);
                }
                else
                {
                    target.Rotate(axis, angle);
                }

                base.OnExecute();
            }
		}
	}
}
