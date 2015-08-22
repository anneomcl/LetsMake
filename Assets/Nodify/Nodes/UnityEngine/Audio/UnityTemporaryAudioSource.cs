using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    public class UnityTemporaryAudioSource : MonoBehaviour
    {
        private AudioSource m_audioSource;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(!m_audioSource.isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}