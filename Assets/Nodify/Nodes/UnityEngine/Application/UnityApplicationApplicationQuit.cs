using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Application/Application Quit", "Application.ApplicationQuit")]
    public class UnityApplicationApplicationQuit : Node 
    {
        protected override void OnExecute()
        {
            Application.Quit();
            base.OnExecute();
        }
    }
}
