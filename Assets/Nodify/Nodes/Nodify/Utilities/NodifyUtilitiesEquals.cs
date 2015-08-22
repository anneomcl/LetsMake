using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Utilities/Equals", "Utilities.Equals")]
	public class NodifyUtilitiesEquals : Node 
	{
        [Expose]
        public object obj1;

        [Expose]
        public object obj2;
		
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
            if(obj1 == null && obj2 == null) { return; }

            if(obj1.Equals(obj2))
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
