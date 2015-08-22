using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Utilities/Pool Manager/WarmObject", "PoolManager.WarmObject", "Icons/pool_manager_warm_icon")]
    public class PoolManagerWarmObject : Node
    {
        [Expose]
        public GameObject source;

        [Expose]
        public int count = 1;

        protected override void OnExecute()
        {
            PoolManager.WarmPool(this.source, this.count);
            // fire exposed method: OnComplete();
            base.OnExecute();
        }
    }
}