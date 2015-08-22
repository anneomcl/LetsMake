using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Camera/Get/Camera Background Color", "Camera.GetCameraBackgroundColor", description = "Gets the camera background color")]
    public class UnityCameraGetCameraBackgroundColor : Node
    {
        [Expose]
        public GameObject targetCamera;
        [Expose]
        public Color newBackgroundColor;

        protected override void OnExecute()
        {
            if (targetCamera != null && targetCamera.GetComponent<Camera>() != null)
            {
                newBackgroundColor = targetCamera.GetComponent<Camera>().backgroundColor;
            }

            base.OnExecute();
        }
    }
}
