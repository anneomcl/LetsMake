using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Get/Float", "PlayerPrefs.GetFloat" )]
	public class UnityPlayerPrefsGetFloat : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public float value;

		protected override void OnExecute()
		{
            value = PlayerPrefs.GetFloat(keyName);

			base.OnExecute();
		}
	}
}
