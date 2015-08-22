using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Get/Integer", "Animator.GetInteger")]
	public class UnityAnimatorGetInteger : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        [HideInInspector]
        public int value;

		protected override void OnExecute()
		{
            value = target.GetInteger(stateName);

            base.OnExecute();
		}
	}
}
