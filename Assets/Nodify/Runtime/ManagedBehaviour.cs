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
using UnityEngine;
using System;
using System.Collections;

namespace Nodify.Runtime
{
    /// <summary>
    /// A utility class that provides event delegate callbacks
    /// so we may listen for node firing purposes.
    /// </summary>
	public class ManagedBehaviour : MonoBehaviour 
	{
		public event Action<Collision> CollisionEntered;
		public event Action<Collision> CollisionStayed;
		public event Action<Collision> CollisionExited;

        public event Action<Collision2D> CollisionEntered2D;
        public event Action<Collision2D> CollisionStayed2D;
        public event Action<Collision2D> CollisionExited2D;

		public event Action<Collider> TriggerEntered;
		public event Action<Collider> TriggerStayed;
		public event Action<Collider> TriggerExited;

        public event Action<Collider2D> TriggerEntered2D;
        public event Action<Collider2D> TriggerStayed2D;
        public event Action<Collider2D> TriggerExited2D;

        public event Action MouseDown;
        public event Action MouseUp;
        public event Action MouseEntered;
        public event Action MouseExited;
        public event Action MouseDragged;

        #region Collision Events
        protected void OnCollisionEnter(Collision collision)
		{
			if(CollisionEntered != null)
			{
				CollisionEntered(collision);
			}
		}

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (CollisionEntered2D != null)
            {
                CollisionEntered2D(collision);
            }
        }

		protected void OnCollisionStay(Collision collision)
		{
			if(CollisionStayed != null)
			{
				CollisionStayed(collision);
			}
		}

        protected void OnCollisionStay2D(Collision2D collision)
        {
            if (CollisionStayed2D != null)
            {
                CollisionStayed2D(collision);
            }
        }

		protected void OnCollisionExit(Collision collision)
		{
			if(CollisionExited != null)
			{
				CollisionExited(collision);
			}
		}

        protected void OnCollisionExit2D(Collision2D collision)
        {
            if (CollisionExited2D != null)
            {
                CollisionExited2D(collision);
            }
        }

        #endregion

        #region Trigger Events

        protected void OnTriggerEnter(Collider collision)
		{
			if(TriggerEntered != null)
			{
				TriggerEntered(collision);
			}
		}

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerEntered2D != null)
            {
                TriggerEntered2D(collision);
            }
        }

		protected void OnTriggerStay(Collider  collision)
		{
			if(TriggerStayed != null)
			{
				TriggerStayed(collision);
			}
		}

        protected void OnTriggerStay2D(Collider2D collision)
        {
            if (TriggerStayed2D != null)
            {
                TriggerStayed2D(collision);
            }
        }

        protected void OnTriggerExit(Collider collision)
        {
            if (TriggerExited != null)
            {
                TriggerExited(collision);
            }
        }

		protected void OnTriggerExit2D(Collider2D  collision)
		{
			if(TriggerExited2D != null)
			{
				TriggerExited2D(collision);
			}
		}

        #endregion

#if (UNITY_EDITOR || UNITY_STANDALONE)
        protected void OnMouseDown()
        {
            if(MouseDown != null)
            {
                MouseDown();
            }
        }

        protected void OnMouseUp()
        {
            if(MouseUp != null)
            {
                MouseUp();
            }
        }

        protected void OnMouseEnter()
        {
            if(MouseEntered != null)
            {
                MouseEntered();
            }
        }

        protected void OnMouseExit()
        {
            if(MouseExited != null)
            {
                MouseExited();
            }
        }

        protected void OnMouseDrag()
        {
            if(MouseDragged != null)
            {
                MouseDragged();
            }
        }
        #endif
	}
}
