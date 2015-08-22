using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/GameObject/Get/Transform", "GameObject.GetTransform")]
    public class UnityGameObjectGetTransform : Node
    {
        [Expose]
        public GameObject source;

        [Expose]
        public Transform result;

        protected override void OnExecute()
        {
            this.result = this.source.transform;
            // fire exposed method: OnComplete();
            base.OnExecute();
        }
    }
}