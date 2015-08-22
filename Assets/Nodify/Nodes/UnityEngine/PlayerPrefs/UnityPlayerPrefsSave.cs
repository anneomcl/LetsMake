using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Save", "PlayerPrefs.Save" )]
	public class UnityPlayerPrefsSave : Node 
	{
		protected override void OnExecute()
		{
            PlayerPrefs.Save();

			base.OnExecute();
		}
	}
}
