using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/GameObject/IsNotNull", "GameObject.IsNotNull", "Icons/GameObjectIsNotNull")]
    public class UnityGameObjectIsNotNull : Node
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
            if (this.target != null)
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