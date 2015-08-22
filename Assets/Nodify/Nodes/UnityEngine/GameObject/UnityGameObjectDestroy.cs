using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/GameObject/Destroy", "GameObject.Destroy", "Icons/unity_gameobject_destroy_icon")]
	public class UnityGameObjectDestroy : Node 
	{
        [Expose]
        public GameObject target;

		protected override void OnExecute()
		{
            GameObject.Destroy(target);

			base.OnExecute();
		}
	}
}
