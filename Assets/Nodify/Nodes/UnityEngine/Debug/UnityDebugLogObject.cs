using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Debug/Log Object", "Debug.LogObject", "Icons/unity_debug_log_icon")]
    public class UnityDebugLogObject : Node
    {
        [Expose]
        public object objectToLog;

        protected override void OnExecute()
        {
            Debug.Log(objectToLog);
            base.OnExecute();
        }
    }
}
