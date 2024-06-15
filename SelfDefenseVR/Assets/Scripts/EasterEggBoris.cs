using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggBoris : MonoBehaviour {

    //counts the amount hits Boris's head has taken
    private int hits = 0;
    
    //Boris's body
    public GameObject Boris;
    
    //Boris's box collider for his head
    private GameObject BorisHeadBox;
    
    //the squeak noise
    private AudioSource Squake;

    //gets the squeak noise
    private void Awake()
    {
        Squake = GetComponent<AudioSource>();
    }

    //if the players hasn't hit Boris's head 10 times, squeak
    private void OnTriggerEnter(Collider other)
    {
        if (hits <= 10) {
            Squake.Play();
        } 
    }

    
    private void OnTriggerExit(Collider other)
    {
        //if Boris's has been hit in the head 10 times
        hits = hits + 1;
        if (hits >= 10)
        {
            //give Boris a rigid body
            Rigidbody BorisBody = Boris.AddComponent<Rigidbody>();
            
            //turn off the body's gravity
            BorisBody.useGravity = false;
            
            //lower Boris's mass
            BorisBody.mass = 0.5f;
            
            //Stop Boris's head from being a trigger box collider and become solid and collidable
            BoxCollider BorisHeadBox = GetComponent<BoxCollider>();
            BorisHeadBox.isTrigger = false;
        }
    }
}
