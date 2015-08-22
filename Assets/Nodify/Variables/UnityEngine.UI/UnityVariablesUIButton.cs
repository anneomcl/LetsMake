#if UNITY_4_6 || UNITY_5
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine.UI/Button", "Button")]
	public class UnityVariablesUIButton : GlobalVariable<Button> 
    { 
        [Expose]
        public void OnClick()
        {

        }

        private void Awake()
        {
            if(value != null)
            {
                value.onClick.AddListener(delegate
                {
                    this.Fire("OnClick");
                });
            }
        }
    }
}
#endif