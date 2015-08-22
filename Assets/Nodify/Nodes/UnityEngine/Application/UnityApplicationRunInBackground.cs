using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Application/Run In Background", "Application.RunInBackground")]
    public class UnityApplicationRunInBackground : Node
    {
        [Expose]
        public bool runInBackground;

        protected override void OnExecute()
        {
            Application.runInBackground = runInBackground;

            base.OnExecute();
        }
    }
}
