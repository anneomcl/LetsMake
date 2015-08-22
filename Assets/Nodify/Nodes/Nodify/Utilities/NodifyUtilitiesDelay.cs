using UnityEngine;
using System.Collections.Generic;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Utilities/Delay", "Utilities.Delay", "Icons/utilities_delay_icon")]
	public class NodifyUtilitiesDelay : Node 
	{
        [Expose]
        public float delayAmount;

        /// <summary>
        /// If consecutive, it will schedule each call one after another.
        /// Otherwise, it will increment each call at the same time.
        /// </summary>
        [Expose]
        public bool consecutive = true;

        private List<NodifyUtilitiesDelayJob> m_delayJobs = new List<NodifyUtilitiesDelayJob>();

        [Expose(true)]
        public void OnElapsed()
        {
            
        }

        private void Update()
        {
            if (consecutive)
            {
                if(m_delayJobs.Count > 0)
                {
                    float elapsed = Time.realtimeSinceStartup - m_delayJobs[0].startedAt;

                    if (elapsed >= delayAmount)
                    {
                        this.Fire("OnElapsed");

                        m_delayJobs.RemoveAt(0);

                        if(m_delayJobs.Count > 0)
                        {
                            m_delayJobs[0].startedAt = Time.realtimeSinceStartup;
                        }
                    }
                }
            }
            else
            {
                for (int i = m_delayJobs.Count - 1; i >= 0; i--)
                {
                    float elapsed = Time.realtimeSinceStartup - m_delayJobs[i].startedAt;

                    if (elapsed >= delayAmount)
                    {
                        this.Fire("OnElapsed");

                        m_delayJobs.RemoveAt(i);
                    }
                }
            }
        }

		protected override void OnExecute()
		{
            NodifyUtilitiesDelayJob job = new NodifyUtilitiesDelayJob();
            job.startedAt = Time.realtimeSinceStartup;
            job.delayAmount = delayAmount;

            if (this.consecutive)
            {
                if(m_delayJobs.Count == 0)
                {
                    m_delayJobs.Add(job);
                }
            }
            else
            {
                m_delayJobs.Add(job);
            }

			base.OnExecute();
		}
	}
    
    public class NodifyUtilitiesDelayJob
    {
        public float startedAt;
        public float delayAmount;
    }
}
