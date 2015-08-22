using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Utilities/Pool Manager/DestroyObject", "PoolManager.DestroyObject", "Icons/pool_manager_destroy_icon")]
    public class UtilitiesPoolManagerDestroyObject : Node
    {
        [Expose]
        public GameObject source;

        protected override void OnExecute()
        {
            PoolManager.ReleaseObject(this.source.gameObject);
            base.OnExecute();
        }
    }
}