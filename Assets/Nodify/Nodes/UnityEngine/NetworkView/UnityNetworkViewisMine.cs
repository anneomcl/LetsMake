using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/NetworkView/IsMine", "NetworkView.IsMine" )]
	public class UnityNetworkViewisMine : Node 
	{
        [Expose]
        public NetworkView targetView;

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
			if(targetView.isMine)
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
