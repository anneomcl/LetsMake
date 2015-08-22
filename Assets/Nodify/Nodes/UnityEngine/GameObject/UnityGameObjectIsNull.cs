using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/GameObject/IsNull", "GameObject.IsNull", "Icons/GameObjectIsNotNull")]
    public class UnityGameObjectIsNull : Node
    {
        [Expose]
        public GameObject target;

        [Expose]
        public void OnTrue()
        {
        }

        [Expose]
        public void OnFalse()
        {
        }

        protected override void OnExecute()
        {
            if (this.target = null)
            {
                this.Fire("OnTrue");
            }
            else
            {
                this.Fire("OnFalse");
            }
            base.OnExecute();
        }
    }
}