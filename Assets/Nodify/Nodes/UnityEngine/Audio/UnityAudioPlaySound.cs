using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Audio/Play Sound", "Audio.PlaySound", "Icons/unity_audio_icon")]
	public class UnityAudioPlaySound : Node 
	{
        [Expose]
        public AudioSource source;

        [Expose]
        public AudioClip audioClip;

		protected override void OnExecute()
		{
            if (audioClip != null)
            {
                source.clip = audioClip;
            }

            source.Play();
		}
	}
}
