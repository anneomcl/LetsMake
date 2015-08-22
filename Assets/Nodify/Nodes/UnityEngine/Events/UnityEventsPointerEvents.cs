#if UNITY_4_6 || UNITY_5

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Events/Pointer Events", "Events.PointerEvents", "Icons/events_pointer_icon")]
	public class UnityEventsPointerEvents : Node 
	{
        private ManagedEventsBehaviour m_uiBehaviour;
        private bool m_registered;

        #region Exposed
        [Expose]
        public GameObject target;

        [Expose]
        public PointerEventData eventData;

        [Expose]
        public void OnPointerDown()
        {
            
        }

        [Expose]
        public void OnPointerUp()
        {
            
        }

        [Expose]
        public void OnPointerEnter()
        {
            
        }

        [Expose]
        public void OnPointerExit()
        {
            
        }

        [Expose]
        public void OnPointerClick()
        {

        }

        [Expose]
        public void OnDrag()
        {
            
        }

        [Expose]
        public void OnDrop()
        {
            
        }

        [Expose]
        public void OnScroll()
        {
            
        }
        #endregion


        private void OnEnable()
        {
            this.RegisterListeners();
        }

        private void RegisterListeners()
        {
            if (target != null)
            {
                if (!m_registered)
                {
                    m_uiBehaviour = (target.GetComponent<ManagedEventsBehaviour>()) ? target.GetComponent<ManagedEventsBehaviour>() : target.AddComponent<ManagedEventsBehaviour>();
                    m_uiBehaviour.PointerDown += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnPointerDown"); };
                    m_uiBehaviour.PointerUp += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnPointerUp"); };
                    m_uiBehaviour.PointerEnter += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnPointerEnter"); };
                    m_uiBehaviour.PointerExit += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnPointerExit"); };
                    m_uiBehaviour.Drag += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnDrag"); };
                    m_uiBehaviour.Drop += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnDrop"); };
                    m_uiBehaviour.Scroll += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnScroll"); };
                    m_uiBehaviour.PointerClick += delegate(PointerEventData eventData) { this.eventData = eventData; Fire("OnPointerClick"); };

                    m_registered = true;
                }
            }
            else
            {
                throw new System.Exception("Target has not been assigned!");
            }
        }

	}
}

#endif