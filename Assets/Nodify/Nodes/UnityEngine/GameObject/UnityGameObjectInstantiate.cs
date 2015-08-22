using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/GameObject/Instantiate", "GameObject.Instantiate", "Icons/unity_gameobject_instantiate_icon")]
	public class UnityGameObjectInstantiate : Node 
	{
        [Expose]
        public GameObject prefab;

        [Expose]
        public Vector3 spawnPosition;

        [Expose]
        public Quaternion spawnRotation = Quaternion.identity;

		protected override void OnExecute()
		{
            GameObject.Instantiate(prefab, spawnPosition, spawnRotation);

			base.OnExecute();
		}
	}
}
