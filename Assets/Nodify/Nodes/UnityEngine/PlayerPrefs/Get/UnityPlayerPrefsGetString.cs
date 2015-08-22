using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Get/String", "PlayerPrefs.GetString" )]
	public class UnityPlayerPrefsGetString : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public string value;

		protected override void OnExecute()
		{
            value = PlayerPrefs.GetString(keyName);

			base.OnExecute();
		}
	}
}
