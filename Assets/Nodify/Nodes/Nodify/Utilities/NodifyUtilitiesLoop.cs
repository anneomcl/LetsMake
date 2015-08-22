using UnityEngine;
using System.Collections.Generic;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Utilities/Loop", "Utilities.Loop", "Icons/utilities_loop_icon")]
	public class NodifyUtilitiesLoop : Node 
	{
        [Expose]
        public int numberOfTimes;

        [Expose][HideInInspector]
        public int currentIndex;

        [Expose(true)]
		public void OnLoopPass() { }

		protected override void OnExecute()
		{
			for(int i = 0; i < numberOfTimes; i++)
            {
                this.currentIndex = i;
                this.Fire("OnLoopPass");
            }
		}
	}
}
