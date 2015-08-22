using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Set/Bool", "Animator.SetBool")]
	public class UnityAnimatorSetBool : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        public bool state;

		protected override void OnExecute()
		{
            target.SetBool(stateName, state);

            base.OnExecute();
		}
	}
}
