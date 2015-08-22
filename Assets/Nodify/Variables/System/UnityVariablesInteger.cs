using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/System/Integer", "Integer")]
	public class UnityVariablesInteger : GlobalVariable<int> 
    {
        public IntegerExecuteMode executeMode = IntegerExecuteMode.INCREMENT;
    
        public enum IntegerExecuteMode
        {
            NONE,
            INCREMENT,
            DECREMENT
        }

        protected override void OnExecute()
        {
            switch(executeMode)
            {
                case IntegerExecuteMode.INCREMENT:
                    SetValue(this.keyName, this.value + 1);
                    break;
                case IntegerExecuteMode.DECREMENT:
                    SetValue(this.keyName, this.value - 1);
                    break;
            }

            base.OnExecute();
        }
    }

}
