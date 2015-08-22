using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Set Trigger", "Animator.SetTrigger")]
	public class UnityAnimatorSetTrigger : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string triggerName;


		protected override void OnExecute()
		{
            target.SetTrigger(triggerName);
		}
	}
}
