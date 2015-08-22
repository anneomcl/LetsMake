using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Utilities/Pool Manager/SpawnObject", "PoolManager.SpawnObject", "Icons/pool_manager_spwan_icon")]
    public class PoolManagerSpawnObject : Node
    {
        [Expose]
        public GameObject source;

        [Expose]
        public GameObject target;

        [Expose]
        public Vector3 position = Vector3.zero;

        [Expose]
        public Vector3 rotation = Vector3.zero;

        protected override void OnExecute()
        {
            this.target = PoolManager.SpawnObject(this.source, this.position, this.rotation);
            base.OnExecute();
        }
    }
}