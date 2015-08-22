/**
Copyright (c) <2014>, <Devon Klompmaker>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/
#if UNITY_4_6 || UNITY_5

using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace Nodify.Runtime
{
    public class ManagedEventsBehaviour : ManagedBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, IDragHandler, IDropHandler, IScrollHandler, IPointerClickHandler
    {
        public event Action<PointerEventData> PointerDown;
        public event Action<PointerEventData> PointerUp;
        public event Action<PointerEventData> PointerExit;
        public event Action<PointerEventData> PointerEnter;
        public event Action<PointerEventData> PointerClick;
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData> Drop;
        public event Action<PointerEventData> Scroll;

        

        public void OnPointerDown(PointerEventData eventData)
        {
            if(PointerDown != null)
            {
                PointerDown(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(PointerUp != null)
            {
                PointerUp(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(PointerExit != null)
            {
                PointerExit(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(PointerEnter != null)
            {
                PointerEnter(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Drag != null)
            {
                Drag(eventData);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(Drop != null)
            {
                Drop(eventData);
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if(Scroll != null)
            {
                Scroll(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(PointerClick != null)
            {
                PointerClick(eventData);
            }
        }
    }
}

#endif
