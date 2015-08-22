using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Set/Float", "Animator.SetFloat")]
	public class UnityAnimatorSetFloat : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        public float state;

		protected override void OnExecute()
		{
            target.SetFloat(stateName, state);

            base.OnExecute();
		}
	}
}
