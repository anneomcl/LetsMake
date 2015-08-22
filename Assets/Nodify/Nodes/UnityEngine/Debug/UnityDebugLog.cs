using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Debug/Log", "Debug.Log", "Icons/unity_debug_log_icon")]
    public class UnityDebugLog : Node
    {
        [Expose]
        public string textToLog;

        protected override void OnExecute()
        {
            Debug.Log(textToLog);
            base.OnExecute();
        }
    }
}
