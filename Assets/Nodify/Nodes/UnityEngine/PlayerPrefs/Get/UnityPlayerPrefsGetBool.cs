using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Get/Bool", "PlayerPrefs.GetBool" )]
	public class UnityPlayerPrefsGetBool : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public bool value;

		protected override void OnExecute()
		{
            value = System.Convert.ToBoolean(PlayerPrefs.GetInt(keyName));

			base.OnExecute();
		}
	}
}
