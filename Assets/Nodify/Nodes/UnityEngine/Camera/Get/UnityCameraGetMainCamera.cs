using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Camera/Get/Main Camera", "Camera.GetMainCamera", description = "Gets the main camera")]
    public class UnityCameraGetMainCamera : Node
    {
        [Expose]
        public GameObject mainCamera;

        protected override void OnExecute()
        {
            mainCamera = Camera.main.gameObject;

            base.OnExecute();
        }
    }
}
