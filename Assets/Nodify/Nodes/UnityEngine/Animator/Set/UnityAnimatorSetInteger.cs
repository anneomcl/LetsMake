using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Set/Integer", "Animator.SetInteger")]
	public class UnityAnimatorSetInteger : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        public int state;

		protected override void OnExecute()
		{
            target.SetFloat(stateName, state);

            base.OnExecute();
		}
	}
}
