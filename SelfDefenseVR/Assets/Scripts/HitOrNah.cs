using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOrNah : MonoBehaviour {
    //Creates booleans that helps the DisableUponCollision script understand when which hand enters the corresponding pad
    public bool leftControllerHit;
    public bool rightControllerHit;

    //When the pad is entered by the specified hand change the corresponding hands boolean to true
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("leftHand")) {
            leftControllerHit = true;
        }
        else if (other.gameObject.CompareTag("rightHand")) {
            rightControllerHit = true;
        }
    }
    
    //When the pad is exited by the specified hand change the corresponding hands boolean to false
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("leftHand")) {
            leftControllerHit = false;
        } else if (other.gameObject.CompareTag("rightHand")) {
            rightControllerHit = false;
        }
    }
}
