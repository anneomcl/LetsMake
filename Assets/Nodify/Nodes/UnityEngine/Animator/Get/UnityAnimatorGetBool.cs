using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Get/Bool", "Animator.GetBool")]
	public class UnityAnimatorGetBool : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        [HideInInspector]
        public bool state;

        [Expose]
        [HideInInspector]
        public bool inversedState;

		protected override void OnExecute()
		{
            state = target.GetBool(stateName);
            inversedState = !state;

            base.OnExecute();
		}
	}
}
