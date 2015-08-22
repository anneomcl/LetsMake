using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Transform/Move", "Transform.Move", "Icons/icon_Transform_Move")]
    public class UnityTransformMove : Node
    {
        [Expose]
        public Transform target;

        [Expose]
        public Vector3 direction;

        [Expose]
        public float speed = 1f;

        [Expose]
        public bool useDeltaTime;

        [Expose]
        public bool relative;

        protected override void OnExecute()
        {
            Vector3 targetDir = direction;

            if (relative)
            {
                targetDir = target.TransformDirection(targetDir * this.speed);
            }

            if (useDeltaTime)
            {
                target.position += (targetDir * this.speed) * Time.deltaTime;
            }
            else
            {
                target.position += targetDir * this.speed;
            }

            base.OnExecute();
        }
    }
}