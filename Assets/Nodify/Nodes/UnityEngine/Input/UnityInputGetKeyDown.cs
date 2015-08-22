using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Input/Get Key Down", "Input.GetKeyDown", "Icons/blank-keyboard-key-icon")]
    public class UnityInputGetKeyDown : Node
    {
        [Expose]
        public KeyCode keyCode;

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
            if (Input.GetKeyDown(keyCode))
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