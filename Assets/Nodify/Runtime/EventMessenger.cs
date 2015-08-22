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
using System.Collections.Generic;

namespace Nodify.Runtime
{
    /// <summary>
    /// A helper utility to listen and fire events.
    /// </summary>
    public class EventMessenger
    {
        private static Dictionary<string, List<Action<EventArgs>>> registeredEventHandlers = new Dictionary<string, List<Action<EventArgs>>>();

        /// <summary>
        /// Fires the event with the sepcified args.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="args">Arguments.</param>
        public static void Fire(string eventName, EventArgs args)
        {
            if(registeredEventHandlers.ContainsKey(eventName))
            {
                for(int i = 0; i < registeredEventHandlers[eventName].Count; i++)
                {
                    registeredEventHandlers[eventName][i](args);
                }
            }
        }

        /// <summary>
        /// Registers a callback listener for the specified event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        public static void Register(string eventName, Action<EventArgs> callback)
        {
            if(!registeredEventHandlers.ContainsKey(eventName))
            {
                registeredEventHandlers.Add(eventName, new List<Action<EventArgs>>());
            }

            if(!registeredEventHandlers[eventName].Contains(callback))
            {
                registeredEventHandlers[eventName].Add(callback);
            }
        }

		/// <summary>
		/// Unregisters a callback listener for the specified event.
		/// </summary>
		/// <param name="eventName">Event name.</param>
		/// <param name="callback">Callback.</param>
		public static void Unregister(string eventName, Action<EventArgs> callback)
		{
			if(registeredEventHandlers.ContainsKey(eventName))
			{
				registeredEventHandlers[eventName].Remove(callback);
			}
		}
    }
}
