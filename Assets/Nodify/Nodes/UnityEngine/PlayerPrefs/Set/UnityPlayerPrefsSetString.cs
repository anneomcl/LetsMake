using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Set/String", "PlayerPrefs.SetString" )]
	public class UnityPlayerPrefsSetString : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public string value;

		protected override void OnExecute()
		{
            PlayerPrefs.SetString(keyName, value);

			base.OnExecute();
		}
	}
}
