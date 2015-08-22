using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Set/Int", "PlayerPrefs.SetInt" )]
	public class UnityPlayerPrefsSetInt : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public int value;

		protected override void OnExecute()
		{
            PlayerPrefs.SetInt(keyName, value);

			base.OnExecute();
		}
	}
}
