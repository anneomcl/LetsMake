using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/PlayerPrefs/Has Key", "PlayerPrefs.HasKey" )]
	public class UnityPlayerPrefsHasKey : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public void OnTrue()
        {
            
        }

        [Expose]
        public void OnFalse()
        {
            
        }

		protected override void OnExecute()
		{
            if(PlayerPrefs.HasKey(keyName))
            {
                this.Fire("OnTrue");
            }
            else
            {
                this.Fire("OnFalse");
            }

			base.OnExecute();
		}
	}
}
