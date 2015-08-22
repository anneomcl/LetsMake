using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/System/Boolean", "Boolean")]
	public class UnityVariablesBoolean : GlobalVariable<bool> 
    {
        public bool ToggleOnExecute = true;

        [Expose]
        public bool Inverse
        {
            get
            {
                return !value;
            }
        }

        protected override void OnExecute()
        {
            if(ToggleOnExecute)
            {
                bool toggleValue = !this.value;
                SetValue(keyName, toggleValue);
            }

            base.OnExecute();
        }
    }
}
