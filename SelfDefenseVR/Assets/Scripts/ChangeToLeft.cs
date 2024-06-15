using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToLeft : MonoBehaviour {

    //the paths for the punches
    public GameObject LeftPath;
    public GameObject RightPath;
    
    //Boris's body
    public GameObject Boris;
    
    //The Animations for Boris
    private Animator BorisAnimator;
    
    //The sound for punching
    private AudioSource punchSound;
    
    //audio clip for controller vibration
    public AudioClip HapticFeedback;
    
    //audio clip for Boris's voice clips
    public AudioClip BorisVoice;
    
    //makes sure that Boris's voice doesn't overlap
    private bool hasPlayed = false;
    
    //the path for punching
    private GameObject path;


    private void Awake()
    {
        BorisAnimator = Boris.GetComponent<Animator>();
        punchSound = GetComponent<AudioSource>();
    }

    //plays punch sound when player hits the pad
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
        //makes sure that the user used the left hand to punch the pad
        if (other.gameObject.CompareTag("leftHand"))
        {
            //switches hands and path. Plays Boris's next voice line
            BorisAnimator.SetBool("HitLeftPad", true);

            LeftPath.SetActive(false);
            RightPath.SetActive(true);

            BorisAnimator.SetBool("HitRightPad", false);
            hasPlayed = false;
            punchSound.PlayOneShot(BorisVoice);
        }
    }
}
