using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/Camera", "Camera")]
	public class UnityVariablesCamera : GlobalVariable<Camera> 
    { 
        [Expose]
        public float fieldOfView
        {
            get
            {
                if(value != null)
                {
                    return value.fieldOfView;
                }

                return -1;
            }
            set
            {
                if(this.value != null)
                {
                    this.value.fieldOfView = value;
                }
            }
        }

        [Expose]
        public Color backgroundColor
        {
            get
            {
                if(value != null)
                {
                    return value.backgroundColor;
                }

                return Color.clear;
            }
            set
            {
                if(this.value != null)
                {
                    this.value.backgroundColor = value;
                }
            }
        }
    }
}
