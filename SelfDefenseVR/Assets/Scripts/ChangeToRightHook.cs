using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToRightHook : MonoBehaviour {

    //the paths for the punches
    public GameObject LeftPath;
    public GameObject RightPath;
    
    //Boris's body
    public GameObject Boris;
    
    //The animations for Boris
    private Animator BorisAnimator;
    
    //The sound for punching
    private AudioSource punchSound;
    
    //Audio clip for controller vibration
    public AudioClip HapticFeedback;
    
    //audio clip for Boris's voice clips
    public AudioClip BorisVoice;
    
    //makes sure that Boris's voice doesn't overlap
    private bool hasPlayed = false;


    private void Awake()
    {
        BorisAnimator = Boris.GetComponent<Animator>();
        punchSound = GetComponent<AudioSource>();
    }

    //plays punch sound when player hits pad
    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed == false) {
            punchSound.Play();
            hasPlayed = true;
        }
    
        //controller vibrates when pad is hit
        OVRHapticsClip hapticsClip = new OVRHapticsClip(HapticFeedback);
        //decides what hand to vibrate on
        if (other.gameObject.CompareTag("leftHand")) {
            OVRHaptics.LeftChannel.Preempt(hapticsClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //makes sure the user used the left hand to hit the pad
        if (other.gameObject.CompareTag("leftHand"))
        {
            //switches hands and path. Plays Boris's next voice line
            BorisAnimator.SetBool("HitRightPad", true);

            LeftPath.SetActive(true);
            RightPath.SetActive(false);

            BorisAnimator.SetBool("HitLeftPad", false);
            hasPlayed = false;
            punchSound.PlayOneShot(BorisVoice);
        }
    }
}
