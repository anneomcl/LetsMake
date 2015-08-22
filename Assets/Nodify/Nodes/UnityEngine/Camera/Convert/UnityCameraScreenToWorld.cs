using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Camera/Convert/ScreenToWorld", "Camera.ScreenToWorld")]
    public class UnityCameraScreenToWorld : Node
    {
        [Expose]
        public Camera targetCamera;

        [Expose]
        public float screenX = 0f;

        [Expose]
        public float screenY = 0f;

        [Expose]
        public float depth = 10f;

        [Expose]
        public Vector3 result = Vector3.zero;

        private Vector3 screenVectorPos = Vector3.zero;

        protected override void OnExecute()
        {
            this.screenVectorPos.x = this.screenX;
            this.screenVectorPos.y = this.screenY;
            this.screenVectorPos.z = this.depth;

            if (this.targetCamera == null)
            {
                this.result = Camera.main.ScreenToWorldPoint(this.screenVectorPos);
            }
            else
            {
                this.result = this.targetCamera.ScreenToWorldPoint(this.screenVectorPos);
            }
            // fire exposed method: OnComplete();
            base.OnExecute();
        }
    }
}