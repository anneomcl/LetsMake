using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Get/Int", "PlayerPrefs.GetInt" )]
	public class UnityPlayerPrefsGetInt : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public int value;

		protected override void OnExecute()
		{
            value = PlayerPrefs.GetInt(keyName);

			base.OnExecute();
		}
	}
}
