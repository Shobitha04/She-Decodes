using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToLeftHook : MonoBehaviour {

    //the paths for the punches
    public GameObject LeftPath;
    public GameObject RightPath;
    
    //Boris's body
    public GameObject Boris;
    
    //The animations for Boris
    private Animator BorisAnimator;
    
    //The sound for punching
    private AudioSource punchSound;
   
    //audio clip for controller vibration
    public AudioClip HapticFeedback;
    
    //audio clip for Boris's voice clips
    public AudioClip BorisVoice;
    
    //make sure that Boris's voice doesn't overlap
    private bool hasPlayed = false;

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

        //contoller vibrates when pad is hit
        OVRHapticsClip hapticsClip = new OVRHapticsClip(HapticFeedback);
        //checks when hand hit the pad and makes that hand vibrate
        if (other.gameObject.CompareTag("rightHand")) {
            OVRHaptics.RightChannel.Preempt(hapticsClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //makes sure that the use used the right hand to punch the pad
        if (other.gameObject.CompareTag("rightHand"))
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
