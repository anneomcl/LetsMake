using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Input/Get Mouse Button Down", "Input.GetMouseButtonDown")]
    public class UnityInputGetMouseButtonDown : Node
    {
        [Expose]
        public int button;

        [Expose(true)]
        public void OnTrue()
        {
            
        }

        [Expose]
        public void OnFalse()
        {
            
        }

        protected override void OnExecute()
        {
			if(Input.GetMouseButtonDown(button))
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