using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/WWW/Get", "WWW.Get", "Icons/WWW")]
    public class UnityWWWGet : Node
    {
        [Expose]
        public string url;

        [Expose]
        public string errorMessage = "";

        [Expose]
        public string result;

        [Expose(true)]
        public void OnDone() { }

        [Expose]
        public void OnError() { }

        protected override void OnExecute()
        {
            WWW www = new WWW(url);
            StartCoroutine(WaitForResult(www));

            base.OnExecute();
        }

        private IEnumerator WaitForResult(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                this.result = www.text;
                this.Fire("OnDone");
            }
            else
            {
                this.errorMessage = www.error;
                this.Fire("OnError");
            }
        }
    }
}