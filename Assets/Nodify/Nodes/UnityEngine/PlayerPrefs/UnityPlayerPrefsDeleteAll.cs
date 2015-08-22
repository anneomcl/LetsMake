using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/DeleteAll", "PlayerPrefs.DeleteAll" )]
	public class UnityPlayerPrefsDeleteAll : Node 
	{
		protected override void OnExecute()
		{
            PlayerPrefs.DeleteAll();

			base.OnExecute();
		}
	}
}
