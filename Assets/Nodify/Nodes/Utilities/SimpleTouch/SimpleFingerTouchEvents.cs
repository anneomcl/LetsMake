using Nodify.Runtime;
using System.Collections;
using UnityEngine;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Utilities/Simple Finger Touch/Events", "SimpleFingerTouch.Events", "Icons/touch_events_icon")]
    public class SimpleFingerTouchEvents : Node
    {
        [Expose]
        [Tooltip("Set finger ID (0..9) for touch event")]
        public int fingerID = 0;

        [Expose]
        public TTouchEvent touchEvent;

        [Expose]
        public void OnTouchDown() { }

        [Expose]
        public void OnTouchHold() { }

        [Expose]
        public void OnTouchMove() { }

        [Expose]
        public void OnTouchUp() { }

        private void Start()
        {
            SimpleFingerTouch.instance.touchEvents[this.fingerID].OnTouchDown = _OnTouchDown;
            SimpleFingerTouch.instance.touchEvents[this.fingerID].OnTouchHold = _OnTouchHold;
            SimpleFingerTouch.instance.touchEvents[this.fingerID].OnTouchMove = _OnTouchMove;
            SimpleFingerTouch.instance.touchEvents[this.fingerID].OnTouchUp = _OnTouchUp;
        }

        protected override void OnExecute()
        {
            // fire exposed method: OnComplete();
            base.OnExecute();
        }

        private void _OnTouchDown(TTouchEvent touchEvent)
        {
            this.touchEvent = touchEvent;
            this.Fire("OnTouchDown");
        }

        private void _OnTouchHold(TTouchEvent touchEvent)
        {
            this.touchEvent = touchEvent;
            this.Fire("OnTouchHold");
        }

        private void _OnTouchMove(TTouchEvent touchEvent)
        {
            this.touchEvent = touchEvent;
            this.Fire("OnTouchMove");
        }

        private void _OnTouchUp(TTouchEvent touchEvent)
        {
            this.touchEvent = touchEvent;
            this.Fire("OnTouchUp");
        }
    }
}