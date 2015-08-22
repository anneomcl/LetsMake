using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Nodify/Node Group/Get/Parent GameObject", "NodeGroup.GetParentGameObject", "Icons/unity_gameobject_parent")]
    public class NodeGroupGetParent : Node
    {
        [Expose]
        public string parentName = "";

        [Expose]
        public GameObject resultParent;

        [Expose]
        public void IsNull()
        {
        }

        [Expose]
        public void IsNotNull()
        {
        }

        protected override void OnExecute()
        {
            if (this.parent.transform != null)
            {
                //this.resultParent = this.parent.transform.parent.gameObject;  <---- doesn't work
                this.resultParent = this.transform.parent.parent.gameObject;
            }

            if (this.resultParent == null)
            {
                this.Fire("IsNull");
            }
            else
            {
                this.parentName = this.resultParent.name;
                this.Fire("IsNotNull");
            }
            // fire exposed method: OnComplete();
            base.OnExecute();
        }
    }
}