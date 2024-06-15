using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorisHurtSound : MonoBehaviour {

    //Boris's audio files for when the Easter Egg is activated
    public AudioClip Ow1;
    public AudioClip Ow2;
    public AudioClip Ow3;
    public AudioClip Ow4;
    public AudioClip Ow5;
    public AudioClip Ow6;

    //An audiosource for the audioclips to play through
    private AudioSource BorisHurt;
    
    //keeps track of what audioclip to play
    private int hit = 1;

    //assigns the Audiosource to the variable name
    private void Awake()
    {
        BorisHurt = GetComponent<AudioSource>();
    }

    private void OnCollisionExit(Collision collision)
    {
        //makes sure that the user is colliding with Boris's rigidbody
        if (collision.gameObject.CompareTag("rightHand") || collision.gameObject.CompareTag("leftHand") || collision.gameObject.CompareTag("Katana")) {
            //Stops the audioclips from overlapping
            BorisHurt.Stop();
            //plays the next audio clip
            if (hit == 1) {
                BorisHurt.PlayOneShot(Ow1);
                hit++;
            } else if (hit == 2) {
                BorisHurt.PlayOneShot(Ow2);
                hit++;
            } else if (hit == 3) {
                BorisHurt.PlayOneShot(Ow3);
                hit++;
            } else if (hit == 4) {
                BorisHurt.PlayOneShot(Ow4);
                hit++;
            } else if (hit == 5) {
                BorisHurt.PlayOneShot(Ow5);
                hit++;
            } else if (hit == 6) {
                BorisHurt.PlayOneShot(Ow6);
                hit = 1;
            }
        }
    }
}
