using UnityEngine;
using System;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Audio/Create Sound", "Audio.CreateSound", "Icons/unity_audio_icon")]
	public class UnityAudioCreateSound : Node 
	{
        [Expose]
        public AudioClip audioClip;

        [Expose]
        public bool destroyWhenFinished = true;

		protected override void OnExecute()
		{
            GameObject audioSource = new GameObject("TemporarySound[" + audioClip.name + "]", new Type[] { typeof(AudioSource)});
            audioSource.GetComponent<AudioSource>().clip = audioClip;
            audioSource.GetComponent<AudioSource>().Play();

            if(destroyWhenFinished)
            {
                audioSource.AddComponent<UnityTemporaryAudioSource>();
            }
		}
	}
}
