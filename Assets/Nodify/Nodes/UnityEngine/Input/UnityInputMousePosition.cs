using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Input/Mouse Position", "Input.MousePosition", "Icons/mouse_pointer_position")]
    public class UnityInputMousePosition : Node
    {
        [Expose]
        public float x = 0;

        [Expose]
        public float y = 0;

        protected override void OnExecute()
        {
            this.x = Input.mousePosition.x;
            this.y = Input.mousePosition.y;

            // fire exposed method: OnComplete();
            base.OnExecute();
        }
    }
}