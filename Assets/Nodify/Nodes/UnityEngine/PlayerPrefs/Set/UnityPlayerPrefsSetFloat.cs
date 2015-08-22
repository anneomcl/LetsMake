using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Set/Float", "PlayerPrefs.SetFloat" )]
	public class UnityPlayerPrefsSetFloat : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public float value;

		protected override void OnExecute()
		{
            PlayerPrefs.SetFloat(keyName, value);

			base.OnExecute();
		}
	}
}
