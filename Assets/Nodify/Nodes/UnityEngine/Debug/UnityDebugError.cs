using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Debug/Error", "Debug.Error", "Icons/unity_debug_error_icon")]
    public class UnityDebugError : Node
    {
        protected override void OnExecute()
    	{
			throw new System.Exception("This is an intential exception raised by Debug.Error Node!");
        }
    }
}
