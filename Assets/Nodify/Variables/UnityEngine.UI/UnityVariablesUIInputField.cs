using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine.UI/InputField", "InputField")]
	public class UnityVariablesUIInputField : GlobalVariable<InputField> 
    { 
        [Expose]
        public string text
        {
            get
            {
                if (value != null)
                {
                    return value.text;
                }

                return string.Empty;
            }
            set
            {
                if (this.value != null)
                {
                    this.value.text = value;
                }
            }
        }

        [Expose]
        public void OnValueChange()
        {

        }

        private void Awake()
        {
            if(value != null)
            {
                value.onValueChange.AddListener(delegate(string newText) {
                    this.Fire("OnValueChange");
                });
            }
        }
    }
}
