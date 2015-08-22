using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Animator/Play", "Animator.Play", "Icons/unity_animator_play_icon")]
	public class UnityAnimatorPlay : Node 
	{
        [Expose]
        public Animator target;

        [Expose]
        public string stateName;


		protected override void OnExecute()
		{
            target.Play(stateName);
		}
	}
}
