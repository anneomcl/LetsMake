using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Input/Get Mouse Button", "Input.GetMouseButton", "Icons/mouse_icon")]
    public class UnityInputGetMouseButton : Node
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
            if (Input.GetMouseButton(button))
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