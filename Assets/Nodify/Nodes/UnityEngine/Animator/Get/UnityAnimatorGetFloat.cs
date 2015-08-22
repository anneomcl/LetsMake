using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Get/Float", "Animator.GetFloat")]
	public class UnityAnimatorGetFloat : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;

        [Expose]
        [HideInInspector]
        public float value;

		protected override void OnExecute()
		{
            value = target.GetFloat(stateName);

            base.OnExecute();
		}
	}
}
