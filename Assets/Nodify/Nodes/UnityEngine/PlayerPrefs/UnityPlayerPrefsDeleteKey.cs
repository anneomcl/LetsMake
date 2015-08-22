using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/DeleteKey", "PlayerPrefs.DeleteKey" )]
	public class UnityPlayerPrefsDeleteKey : Node 
	{
        [Expose]
        public string keyName;

		protected override void OnExecute()
		{
            PlayerPrefs.DeleteKey(keyName);

			base.OnExecute();
		}
	}
}
