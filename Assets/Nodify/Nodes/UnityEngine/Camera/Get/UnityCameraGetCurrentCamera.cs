using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Camera/Get/Current Camera", "Camera.GetCurrentCamera", description = "Gets the current camera")]
    public class UnityCameraGetCurrentCamera : Node
    {
        [Expose]
        public GameObject currentCamera;

        protected override void OnExecute()
        {
            currentCamera = Camera.current.gameObject;

            base.OnExecute();
        }
    }
}
