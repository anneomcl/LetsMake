using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Toggle/Bool", "Animator.ToggleBool")]
	public class UnityAnimatorToggleBool : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;


		protected override void OnExecute()
		{
            bool currentState = target.GetBool(stateName);

            target.SetBool(stateName, !currentState);

            base.OnExecute();
		}
	}
}
