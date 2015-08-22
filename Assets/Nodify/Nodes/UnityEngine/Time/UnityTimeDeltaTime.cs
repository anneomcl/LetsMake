using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Time/DeltaTime", "Time.DeltaTime" )]
	public class UnityTimeDeltaTime : Node 
	{
        [Expose]
        public float deltaTime;

        private void Update()
        {
            deltaTime = Time.deltaTime;
        }
	}
}
