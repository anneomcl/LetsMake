using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Set/Bool", "PlayerPrefs.SetBool" )]
	public class UnityPlayerPrefsSetBool : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public bool value;

		protected override void OnExecute()
		{
            PlayerPrefs.SetInt(keyName, System.Convert.ToInt32(value));

			base.OnExecute();
		}
	}
}
