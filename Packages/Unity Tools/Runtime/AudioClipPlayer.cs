using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public class AudioClipPlayer : MonoBehaviour
    {
        public AudioClip[] clips;
        public AudioSource audioSource;

        [InspectorButton("PlayRandom")]
        public bool playRandom;
        public void PlayRandom()
        {
            int randomIndex = Random.Range(0, clips.Length);
            audioSource.PlayOneShot(clips[randomIndex]);
        }
    }
}