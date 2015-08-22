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
using System.Collections;

namespace Nodify.Runtime
{
    public class Connection : MonoBehaviour
    {
        public static float kconnectionResetTime = .5f;

        [HideInInspector]
        public ConnectionState state = ConnectionState.IDLE;

        /// <summary>
        /// The amount of time the state was changed.
        /// </summary>
        private float m_timeLastStateChanged = 0;

    	public Anchor owner
    	{
    		get
    		{
    			if(transform.parent != null)
    			{
    				return transform.parent.GetComponent<Anchor>();
    			}

    			return null;
    		}
    	}

        public void SetConnectionState(ConnectionState state)
        {
            this.state = state;

            // Editor only visual updating... resets back to idle every so often.
            if (Application.isEditor)
            {
                m_timeLastStateChanged = Time.realtimeSinceStartup;
                Invoke("ResetConnectionState", kconnectionResetTime);
            }
        }

        private void ResetConnectionState()
        {
            float m_timeDifference = Time.realtimeSinceStartup - m_timeLastStateChanged;

            if (m_timeDifference >= kconnectionResetTime)
            {
                state = ConnectionState.IDLE;
            }
        }
    }

    public enum ConnectionState
    {
        IDLE,
        RUN,
        ERROR
    }
}