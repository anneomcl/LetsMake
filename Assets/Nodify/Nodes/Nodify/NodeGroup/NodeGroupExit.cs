using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Nodify/Group/Exit", "Group.Exit")]
    public class NodeGroupExit : Node
    {
        protected override void OnExecute()
        {
            this.parent.Fire("OnExit");
        }
    }
}