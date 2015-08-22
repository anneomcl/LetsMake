using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Reset Trigger", "Animator.ResetTrigger")]
	public class UnityAnimatorResetTrigger : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string triggerName;


		protected override void OnExecute()
		{
            target.ResetTrigger(triggerName);
		}
	}
}
