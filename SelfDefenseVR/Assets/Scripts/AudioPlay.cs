using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour {

    // keeps the audio from continuing to play once after hitting the object
    public AudioClip HapticFeedback;
    private bool hasPlayed = false; 
    private AudioSource punchSound;

    // Use this for initialization
    void Awake () {
        // grabs the audio source
        punchSound = this.gameObject.GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        // if one of your hands hits the object, then it plays the sound effect
        if((collision.gameObject.CompareTag("rightHand") || collision.gameObject.CompareTag("leftHand")) && !hasPlayed)
        {
            punchSound.Play();
            hasPlayed = true;

            //get haptics in either the right or left hand
            OVRHapticsClip hapticsClip = new OVRHapticsClip(HapticFeedback);

            if (collision.gameObject.CompareTag("leftHand")) {
               OVRHaptics.LeftChannel.Preempt(hapticsClip);
            }
            else {
                OVRHaptics.RightChannel.Preempt(hapticsClip);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // once your hand leaves the object, the sound can play again
        if (collision.gameObject.CompareTag("rightHand") || collision.gameObject.CompareTag("leftHand")) hasPlayed = false;
    }
}
