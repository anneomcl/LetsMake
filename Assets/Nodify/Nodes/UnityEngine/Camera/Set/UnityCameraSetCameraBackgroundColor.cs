using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Camera/Set/Camera Background Color", "Camera.SetCameraBackgroundColor", description = "Sets the camera background color")]
    public class UnityCameraSetCameraBackgroundColor : Node
    {
        [Expose]
        public GameObject targetCamera;
        [Expose]
        public Color newBackgroundColor;

        protected override void OnExecute()
        {
            if (targetCamera != null && targetCamera.GetComponent<Camera>() != null)
            {
                targetCamera.GetComponent<Camera>().backgroundColor = newBackgroundColor;
            }

            base.OnExecute();
        }
    }
}
