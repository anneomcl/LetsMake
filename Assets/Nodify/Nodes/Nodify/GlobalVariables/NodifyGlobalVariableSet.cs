using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Global Variable/Set", "GlobalVariable.Set")]
	public class NodifyGlobalVariableSet : Node 
	{
        [Expose]
        public string keyName;

        [Expose]
        public object value;

		protected override void OnExecute()
		{
            GlobalVariableBase.SetValue(keyName, value);

			base.OnExecute();
		}
	}
}
