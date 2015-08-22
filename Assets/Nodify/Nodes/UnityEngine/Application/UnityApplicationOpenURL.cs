using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Application/Open URL", "Application.OpenURL")]
    public class UnityApplicationOpenURL : Node
    {
        [Expose]
        public string url;

        protected override void OnExecute()
        {
            Application.OpenURL(url);
            base.OnExecute();
        }
    }
}
